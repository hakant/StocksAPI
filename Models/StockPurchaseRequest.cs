namespace StocksCoreApi.Models
{
    public class StockPurchaseRequest
    {
        public string StockName { get; set; }
        public decimal Units { get; set; }
    }
}