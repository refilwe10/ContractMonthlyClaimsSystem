using Microsoft.EntityFrameworkCore;
using ContractMonthlyClaimsSystem.Models;

namespace ContractMonthlyClaimsSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Claim> Claims { get; set; }
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOption<ApplicationDbContext> option) : base(option) { }

        protected override void OnConfiguring(DbContextOptionBuilder optionBuilder)
        {
            if (!optionBuilder.isConfigured)
            {
                //SQLite database file stored locally in the app folder
                optionBuilder.UseSqlServer("Data Source=claims.db");
            }
        }
    }
}