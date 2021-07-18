using Microsoft.EntityFrameworkCore;
using PetProducts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetProducts.Context
{
    public class ProductsContext : DbContext
    {
        public ProductsContext( DbContextOptions<ProductsContext> options ) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating( ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<ProductBrand>().ToTable("Brand");
        }

        public DbSet<PetProducts.Models.Category> Category { get; set; }
    }
}
