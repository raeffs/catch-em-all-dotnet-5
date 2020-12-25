using CatchEmAll.Models;
using CatchEmAll.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatchEmAll.Controllers
{
  [Route("api/auctions")]
  [ApiController]
  public class AuctionController : ControllerBase
  {
    private readonly IAuctionService auctions;

    public AuctionController(IAuctionService auctions)
    {
      this.auctions = auctions;
    }

    [HttpGet(Name = "GetAllAuctions")]
    [Produces(typeof(IEnumerable<AuctionSummary>))]
    public async Task<IActionResult> Get()
    {
      var auctions = await this.auctions.GetSummaries().ToListAsync();
      return this.Ok(auctions);
    }
  }
}
