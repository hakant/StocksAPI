using System;
using System.Collections.Generic;
using MediatR;
using StocksCoreApi.Data;

namespace StocksCoreApi.Domain.LoadSession
{
    public class LoadSessionRequest : IRequest
    {
        public int NumberOfStocks { get; set; }
    }
    public class LoadSessionHandler : IRequestHandler<LoadSessionRequest>
    {
        private readonly StocksContext _context;
        public LoadSessionHandler(StocksContext context)
        {
            _context = context;
        }
        public void Handle(LoadSessionRequest request)
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
                NetLiquidationValue = 10000,
                Cash = 10000
            });

            _context.SaveChanges();
        }
    }
}