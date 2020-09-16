using DataLibrary.Accounts;
using DataLibrary.Cards;
using DataLibrary.Clients;
using DataLibrary.Deposits;
using Microsoft.EntityFrameworkCore;

namespace DbInteraction
{
    public class AppDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Individual> Individuals { get; set; }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<RegularAccount> RegularAccounts { get; set; }
        public DbSet<VIPAccount> VipAccounts { get; set; }

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
        public DbSet<NullDeposit> NullDeposits { get; set; }

        #endregion

        /// <summary>
        /// Метод срабатывающий при настройки конфигурации БД
        /// </summary>
        /// <param name="optionsBuilder">строитель настроек</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BankingSystem;Trusted_Connection=True;");
        }

        /// <summary>
        /// Метод срабатывающий при создании моделей БД
        /// </summary>
        /// <param name="modelBuilder">строитель моделей</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Client>()
                .HasOne(c => c.Account)
                .WithOne(a => a.Client)
                .HasForeignKey<Account>(a => a.ClientId);

            modelBuilder.Entity<Account>()
                .HasOne(e => e.Card).WithOne(e => e.Account)
                .HasForeignKey<Card>(e => e.Id);

            modelBuilder.Entity<Account>()
                .HasOne(e => e.Deposit).WithOne(e => e.Account)
                .HasForeignKey<Deposit>(e => e.Id);

            modelBuilder.Entity<Account>().ToTable("Accounts");
            modelBuilder.Entity<Card>().ToTable("Accounts");
            modelBuilder.Entity<Deposit>().ToTable("Accounts");
        }
    }
}
