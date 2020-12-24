using CatchEmAll.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CatchEmAll.Repositories
{
  internal class QueryRepository : IQueryRepository
  {
    private readonly DataContext context;

    public QueryRepository(DataContext context)
    {
      this.context = context;
    }

    public async Task<int> CreateAsync(Query query)
    {
      this.context.Add(query);
      await this.context.SaveChangesAsync();
      return query.Id;
    }

    public Task<Query> GetAsync(int id)
    {
      return this.context.Queries.Include(x => x.Auctions).SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateQuery(Query query)
    {
      var entity = await this.context.Queries.AsTracking().SingleOrDefaultAsync(x => x.Id == query.Id);

      foreach (var auction in query.Auctions)
      {
        entity.Auctions.Add(auction);
      }

      await this.context.SaveChangesAsync();
    }
  }
}
