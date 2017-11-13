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
    public class StocksController : Controller
    {
        private readonly StocksContext _context;

        public StocksController(StocksContext context)
        {
            _context = context;
        }

        // GET api/stocks
        [HttpGet]
        public StocksDashboardModel Get()
        {
            Random rnd = new Random();
            var model = new StocksDashboardModel();
            var stats = _context.Stats.First();

            model.Cash = stats.Cash;
            model.NetLiquidationValue = stats.NetLiquidationValue;

            var x = new ExpandoObject() as IDictionary<string, Object>;

            foreach (var stock in _context.Stocks)
            {
                decimal change = Convert.ToDecimal(rnd.NextDouble(-0.1, 0.1));
                decimal lastPrice = stock.LastPrice;
                stock.LastPrice = stock.LastPrice + (stock.LastPrice * change);
                stock.Change = stock.LastPrice - lastPrice;
                stock.PercentageChange = change;
                
                x.Add(stock.Name, new {
                    LastPrice = stock.LastPrice,
                    Change = stock.Change,
                    PercentageChange = stock.PercentageChange
                });
            }

            model.Stocks = (ExpandoObject)x;

            _context.SaveChanges();

            return model;
        }

        // GET api/stocks/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/stocks
        [HttpPost]
        public void Post([FromBody]string numberOfStocks)
        {
        }

        // PUT api/stocks/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/stocks/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
