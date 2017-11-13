using System.ComponentModel.DataAnnotations;

namespace StocksCoreApi.Data
{
    public class StockInfo
    {
        [Key]
        public string Name { get; set; }
        public decimal LastClosingPrice { get; set; }
        public decimal LastPrice { get; set; }
        public decimal Change { get; set; }
        public decimal PercentageChange { get; set; }
    }
}