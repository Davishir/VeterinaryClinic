using VeterinaryClinic.Models;
using Microsoft.EntityFrameworkCore;

namespace VeterinaryClinic.Data
{
    public class VeterinaryContext : DbContext
    {
        public VeterinaryContext(DbContextOptions<VeterinaryContext> options) : base(options)
        {
        }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Owner> Owners { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>().ToTable("Animal");
            modelBuilder.Entity<Visit>().ToTable("Visit");
            modelBuilder.Entity<Owner>().ToTable("Owner");
        }
    }
}
