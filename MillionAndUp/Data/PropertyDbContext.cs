using Microsoft.EntityFrameworkCore;
using MillionAndUp.Models;

namespace MillionAndUp.Data
{
    public class PropertyDbContext : DbContext
    {
        public PropertyDbContext(DbContextOptions<PropertyDbContext> options) : base(options) { }

        public DbSet<Property> Property { get; set; }
        public DbSet<Owner> Owner { get; set; }
        public DbSet<PropertyImage> PropertyImage { get; set; }
        public DbSet<PropertyTrace> PropertyTrace { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Property>().HasKey(p => p.IdProperty);
            modelBuilder.Entity<Owner>().HasKey(p => p.IdOwner);
            modelBuilder.Entity<PropertyImage>().HasKey(p => p.IdPropertyImage);
            modelBuilder.Entity<PropertyTrace>().HasKey(p => p.IdPropertyTrace);

            base.OnModelCreating(modelBuilder);
        }
    }
}
