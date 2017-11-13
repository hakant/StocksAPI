using Microsoft.EntityFrameworkCore;

namespace StocksCoreApi.Data
{
    public class StocksContext : DbContext
    {
        public StocksContext(DbContextOptions<StocksContext> options)
            : base(options)
        {
        }

        public DbSet<StockInfo> Stocks { get; set; }

        public DbSet<BankerStats> Stats { get; set; }
    }
}
