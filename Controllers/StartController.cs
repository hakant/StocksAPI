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
        public void Post([FromBody]StockSizeRequest request)
        {
            _context.Stocks.RemoveRange(_context.Stocks);
            _context.Stats.RemoveRange(_context.Stats);
            _context.SaveChanges();

            var random = new Random();
            List<string> stockNames = new List<string> {
                "GOOGL",
                "AMZN",
                "MSFT",
                "AAPL",
                "ADSK",
                "CSCO",
                "FB",
                "NFLX",
                "NI",
                "NVDA",
                "PYPL",
                "QCOM",
                "CRM",
                "TXN",
                "TRIP",
                "VRSN",
                "WDC",
                "DIS",
                "SBUX",
                "CTXS"
            };

            for (int i = 0; i < request.NumberOfStocks; i++)
            {
                var name = stockNames[i];
                var lastClosingPrice = Convert.ToDecimal(random.NextDouble(2, 200));

                _context.Stocks.Add(new StockInfo
                {
                    Name = name,
                    LastClosingPrice = lastClosingPrice,
                    LastPrice = lastClosingPrice,
                    Change = 0,
                    PercentageChange = 0
                });
            }

            _context.Stats.Add(new BankerStats
            {
                Id = 1,
                NetLiquidationValue = 12300,
                Cash = 3400
            });

            _context.SaveChanges();
        }
    }
}
