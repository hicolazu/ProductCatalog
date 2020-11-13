using Microsoft.EntityFrameworkCore;
using ProductCatalog.Models;
using ProductCatalog.Data.Maps;

namespace ProductCatalog.Data
{
    public class StoreDataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
          => optionsBuilder
              .EnableDetailedErrors()
              .EnableSensitiveDataLogging()
              .UseSqlServer(@"Server=localhost,1433;Database=prodcat;User ID=SA;Password=P@ssw0rd");

         protected override void OnModelCreating(ModelBuilder builder)
            => builder
                .ApplyConfiguration(new ProductMap())
                .ApplyConfiguration(new CategoryMap());
    }
}