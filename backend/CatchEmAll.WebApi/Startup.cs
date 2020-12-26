using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CatchEmAll.WebApi
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
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
        .AddDataAccess(this.Configuration.GetConnectionString("DataContext"))
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

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      //app.ApplicationServices.CreateDatabase();
    }
  }
}
