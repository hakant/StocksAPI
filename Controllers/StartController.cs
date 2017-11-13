using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using StocksCoreApi.Data;
using StocksCoreApi.Models;

namespace StocksCoreApi.Controllers
{
    [Route("api/[controller]")]
    public class StartController : Controller
    {
        private readonly StocksContext _context;

        public StartController(StocksContext context)
        {
            _context = context;
        }

        // POST api/start
        [HttpPost]
        public void Post([FromBody]string numberOfStocks)
        {
            _context.Stocks.RemoveRange(_context.Stocks);

            _context.Stocks.Add(new StockInfo {
                Name = "MSFT",
                LastClosingPrice = 250,
                LastPrice = 252.3M,
                Change = 2.3M,
                PercentageChange = 0.08M
            });
            _context.Stocks.Add(new StockInfo {
                Name = "APPL",
                LastClosingPrice = 310,
                LastPrice = 309.3M,
                Change = -0.7M,
                PercentageChange = -0.03M
            });

            _context.Stats.RemoveRange(_context.Stats);
            _context.Stats.Add(new BankerStats {
                Id = 1,
                NetLiquidationValue = 12300,
                Cash = 3400 
            });

            _context.SaveChanges();
        }
    }
}
