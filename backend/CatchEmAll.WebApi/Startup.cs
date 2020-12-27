using CatchEmAll.Options;
using CatchEmAll.Providers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace CatchEmAll.WebApi
{
  public class Startup
  {
    private readonly IConfiguration configuration;

    public Startup(IConfiguration configuration)
    {
      this.configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddOptions<DataAccessOptions>().Bind(this.configuration.GetSection("CatchEmAll:DataAccess"));
      services.AddOptions<WebApiOptions>().Bind(this.configuration.GetSection("CatchEmAll:WebApi"));
      services.AddOptions<SearchQueryUpdateOptions>().Bind(this.configuration.GetSection("CatchEmAll:Domain:SearchQueryUpdate"));

      var webApiOptions = this.configuration.GetSection("CatchEmAll:WebApi").Get<WebApiOptions>();

      services.AddCors(o => o.AddPolicy("DefaultPolicy", builder =>
      {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
      }));

      services.AddControllers();

      services.AddSwaggerGen(config =>
      {
        config.SwaggerDoc("default", new Microsoft.OpenApi.Models.OpenApiInfo
        {
          Title = "default",
          Version = "v1"
        });
      });

      services
          .AddAuthentication(options =>
          {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
          })
          .AddJwtBearer(options =>
          {
            options.Authority = webApiOptions.SpaIssuer;
            options.Audience = webApiOptions.SpaClientId;
            options.TokenValidationParameters = new TokenValidationParameters
            {
              NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
            };
          })
          .AddJwtBearer("m2m-client", options =>
          {
            options.Authority = webApiOptions.M2mIssuer;
            options.Audience = webApiOptions.M2mAudience;
          });

      services
        .AddAuthorization(Options =>
        {
          Options.DefaultPolicy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme, "m2m-client")
            .Build();
        });

      services
        .AddHttpContextAccessor()
        .AddScoped<IIdentity, Identity>();

      services
        .AddDataAccess(this.configuration.GetConnectionString("DataContext"))
        .AddDomain()
        .AddRicardo();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseCors("DefaultPolicy");

      app.UseSwagger();
      app.UseSwaggerUI(config =>
      {
        config.SwaggerEndpoint("/swagger/default/swagger.json", "default");
      });

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
