using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StocksCoreApi.Data
{
    public class TransactionInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal UnitsPurchased { get; set; }
    }
}