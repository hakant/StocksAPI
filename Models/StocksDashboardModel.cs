using System.Collections.Generic;
using System.Dynamic;

namespace StocksCoreApi.Models
{
    public class StocksDashboardModel
    {
        public StocksDashboardModel()
        {
            Stocks = new ExpandoObject();
        }
        public decimal Cash { get; set; }
        public decimal NetLiquidationValue { get; set; }
        public ExpandoObject Stocks { get; set; } 
    }
}