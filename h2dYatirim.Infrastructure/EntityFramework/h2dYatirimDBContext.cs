using h2dYatirim.Domain.Entity;
using h2dYatırım.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace h2dYatırım.DataAccess
{
    public class h2dYatirimDBContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Host=193.111.125.93;Port=5432;Database=h2dYatirim;User Id=postgres;Password=753159hH;");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<CryptoAccount> CryptoAccounts { get; set; }
        public DbSet<InvestmentAccount> InvestmentAccounts { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<AccountMovement> AccountMovements { get; set; }
    }
    
}
