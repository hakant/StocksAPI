using System.ComponentModel.DataAnnotations;

namespace StocksCoreApi.Data
{
    public class BankerStats
    {
        [Key]
        public int Id { get; set; }
        public decimal Cash { get; set; }
        public decimal NetLiquidationValue { get; set; }
    }
}