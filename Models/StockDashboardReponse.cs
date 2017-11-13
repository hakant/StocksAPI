using System.Collections.Generic;
using System.Dynamic;

namespace StocksCoreApi.Models
{
    public class StockDashboardResponse
    {
        public StockDashboardResponse()
        {
            Stocks = new ExpandoObject();
        }
        public decimal Cash { get; set; }
        public decimal NetLiquidationValue { get; set; }
        public ExpandoObject Stocks { get; set; } 
    }
}