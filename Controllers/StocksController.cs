using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using StocksCoreApi.Data;
using StocksCoreApi.Domain.GetStocks;
using StocksCoreApi.Models;

namespace StocksCoreApi.Controllers
{
    [Route("api/[controller]")]
    public class StocksController : Controller
    {
        private readonly IMediator _application;
        private readonly StocksContext _context;

        public StocksController(IMediator application, StocksContext context)
        {
            _application = application;
            _context = context;
        }

        // GET api/stocks
        [HttpGet]
        public async Task<StockDashboardResponse> Get()
        {
            var request = new GetStocksRequest();
            var response = await _application.Send(request);
            
            return new StockDashboardResponse {
                Cash = response.Cash,
                NetLiquidationValue = response.NetLiquidationValue,
                Stocks = response.Stocks
            };
        }

        // POST api/stocks
        [HttpPost]
        public void Post([FromBody] StockPurchaseRequest purchaseRequest)
        {
            var stock = _context.Stocks.FirstOrDefault(s => s.Name == purchaseRequest.StockName);
            if (stock != null && purchaseRequest.Units > 0)
            {
                var stats = _context.Stats.OrderByDescending(s => s.Id).First();
                var requestedValue = stock.LastPrice * purchaseRequest.Units;
                if (requestedValue <= stats.Cash)
                {
                    stats.Cash = stats.Cash - requestedValue;
                    stock.Position = stock.Position + purchaseRequest.Units;
                    _context.Transactions.Add(new TransactionInfo {
                        TransactionDate = DateTime.UtcNow,
                        UnitsPurchased = purchaseRequest.Units,
                        PurchasePrice = stock.LastPrice
                    });
                }
                else 
                {
                    requestedValue = stats.Cash;
                    var units = requestedValue / stock.LastPrice;
                    stats.Cash = stats.Cash - requestedValue;
                    stock.Position = stock.Position + units;
                    _context.Transactions.Add(new TransactionInfo {
                        TransactionDate = DateTime.UtcNow,
                        UnitsPurchased = units,
                        PurchasePrice = stock.LastPrice
                    });
                }

                _context.SaveChanges();
                return;
            }

            throw new Exception("Something is not right.");
        }
    }
}
