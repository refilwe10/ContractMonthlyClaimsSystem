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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // SQLite database file stored locally in the app folder
                optionsBuilder.UseSqlite("Data Source=claims.db");
            }

        }
    }
}