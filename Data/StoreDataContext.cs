using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using ProdutCatalog.Data.Maps;
using ProdutCatalog.Models;

namespace ProdutCatalog.Data
{
    public class StoreDataContext : DbContext
    {
        public DbSet<Product> Products {get; set;}
        public DbSet<Category> Categories {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=192.168.0.13,1433;Database=ProdutCatalog;User ID=SA;Password=12345678");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());

        }

    }
}