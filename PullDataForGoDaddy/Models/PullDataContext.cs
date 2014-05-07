using System.Data.Entity;

namespace PullDataForGoDaddy.Models
{
    public class PullDataContext : DbContext
    {
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<Industry> Industries { get; set; }
    }
}
