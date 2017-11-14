using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using MediatR;
using StocksCoreApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace StocksCoreApi.Domain.GetStocks
{
    public class GetStocksRequest : IRequest<GetStocksResponse>
    { }
    public class GetStocksResponse 
    {
        public GetStocksResponse()
        {
            Stocks = new ExpandoObject();
        }
        public decimal Cash { get; set; }
        public decimal NetLiquidationValue { get; set; }
        public ExpandoObject Stocks { get; set; } 
    }

    public class GetStocksHandler : IAsyncRequestHandler<GetStocksRequest, GetStocksResponse>
    {
        private readonly StocksContext _context;
        public GetStocksHandler(StocksContext context)
        {
            _context = context;
        }
        public async Task<GetStocksResponse> Handle(GetStocksRequest request)
        {
            Random rnd = new Random();
            var model = new GetStocksResponse();
            var stats = await _context.Stats.OrderByDescending(s => s.Id).FirstAsync();

            // Cash
            model.Cash = stats.Cash;
            model.NetLiquidationValue = stats.Cash;

            // Stocks
            var stocks = await _context.Stocks.ToListAsync();
            var x = new ExpandoObject() as IDictionary<string, Object>;

            foreach (var stock in stocks)
            {
                decimal change = Convert.ToDecimal(rnd.NextDouble(-0.1, 0.1));
                decimal lastPrice = stock.LastPrice;
                stock.LastPrice = stock.LastPrice + (stock.LastPrice * change);
                stock.Change = stock.LastPrice - lastPrice;
                stock.PercentageChange = change;
                
                x.Add(stock.Name, new {
                    LastPrice = stock.LastPrice,
                    Change = stock.Change,
                    PercentageChange = stock.PercentageChange,
                    Position = stock.Position
                });

                if (stock.Position > 0){
                    model.NetLiquidationValue += stock.LastPrice * stock.Position;
                }
            }
            model.Stocks = (ExpandoObject)x;

            await _context.SaveChangesAsync();

            return model;
        }
    }
}