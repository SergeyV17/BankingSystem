using BankingSystem.Models.Implementations.Clients;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Models
{
    class AppDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BankingSystem;Trusted_Connection=True;");
        }
    }
}
