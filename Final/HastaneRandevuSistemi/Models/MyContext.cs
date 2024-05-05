using Microsoft.EntityFrameworkCore;

namespace HastaneRandevuSistemi.Models
{
    public class MyContext : DbContext
    {
        public DbSet<KullaniciModel> KullaniciTablosu { get; set; }
        public DbSet<DoktorModel>  DoktorTablosu { get; set; }
        public DbSet<BolumModel> BolumTablosu { get; set; }
        public DbSet<RandevuModel> RandevuTablosu { get; set; }
        public DbSet<DoktorCalismaSaatlariModeli> doktorCalismaSaatlariModeliTablosu { get; set; }
        public DbSet<HastanelerModel> HastaneTablosu { get; set; }
        public DbSet<SehirModel> SehirTablosu { get; set; }
        public DbSet<IlceModel> IlceTablosu { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb; Database=HastaneDB12; Trusted_Connection=True;");
        }
   

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RandevuModel>()
                .HasOne(r => r.Doktor)
                .WithMany(d => d.)
                .HasForeignKey(r => r.DoktorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Diğer ilişkiler ve konfigürasyonlar
        }*/

    }
}
