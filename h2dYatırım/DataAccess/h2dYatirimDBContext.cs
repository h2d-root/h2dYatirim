using h2dYatırım.Entities;
using Microsoft.EntityFrameworkCore;

namespace h2dYatırım.DataAccess
{
    public class h2dYatirimDBContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Host=193.111.125.93;Port=5432;Database=h2dYatirim;User Id=postgres;Password=753159hH;");
        }
        public DbSet<ShareCertificate> ShareCertificates { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
