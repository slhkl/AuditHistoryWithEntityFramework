using Audit.EntityFramework;
using Data.Entities;
using Data.Entities.Audit;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class DataContext : AuditDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<AuditHistory> AuditHistory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("POSTGRE_URI"));
        }
    }
}
