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
    public class TransactionsController : Controller
    {
        private readonly StocksContext _context;

        public TransactionsController(StocksContext context)
        {
            _context = context;
        }

        // GET api/transactions
        [HttpGet]
        public TransactionsResponse Get()
        {
            var model = new TransactionsResponse();

            var x = new ExpandoObject() as IDictionary<string, Object>;

            foreach (var transaction in _context.Transactions.OrderByDescending(t => t.TransactionDate))
            {   
                x.Add(transaction.Id.ToString(), new {
                    Date = transaction.TransactionDate,
                    Price = transaction.PurchasePrice,
                    Units = transaction.UnitsPurchased
                });
            }

            model.Transactions = (ExpandoObject)x;
            return model;
        }
    }
}
