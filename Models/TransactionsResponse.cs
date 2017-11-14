using System.Collections.Generic;
using System.Dynamic;

namespace StocksCoreApi.Models
{
    public class TransactionsResponse
    {
        public TransactionsResponse()
        {
            Transactions = new ExpandoObject();
        }
        
        public ExpandoObject Transactions { get; set; } 
    }
}