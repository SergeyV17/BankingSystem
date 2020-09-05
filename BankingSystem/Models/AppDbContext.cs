using BankingSystem.Models.Implementations.Accounts;
using BankingSystem.Models.Implementations.BankServices.CardService;
using BankingSystem.Models.Implementations.BankServices.DepositService;
using BankingSystem.Models.Implementations.Clients;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Models
{
    class AppDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Individual> Individuals { get; set; }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<RegularAccount> RegularAccounts { get; set; }
        public DbSet<VipAccount> VipAccounts { get; set; }

        #region Cards

        public DbSet<Card> Cards { get; set; }
        public DbSet<VisaClassic> VisaClassics { get; set; }
        public DbSet<VisaBlack> VisaBlacks { get; set; }
        public DbSet<VisaPlatinum> VisaPlatinums { get; set; }
        public DbSet<VisaCorporate> VisaCorporates { get; set; }

        #endregion

        #region Deposits

        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<DefaultDeposit> DefaultDeposits { get; set; }

        #endregion


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BankingSystem;Trusted_Connection=True;");
        }
    }
}
