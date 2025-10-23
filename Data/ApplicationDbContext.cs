using Microsoft.EntityFrameworkCore;
using ContractMonthlyClaimsSystem.Models;

namespace ContractMonthlyClaimsSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Claim> Claims { get; set; }

        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlite("Data Source=claims.db"); // ✅ your local DB file
            }
        }
    }
}

    
