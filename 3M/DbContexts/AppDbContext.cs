using _3M.DataModels;
using _3M.DataModels.Account;
using _3M.DataModels.Configuration;
using _3M.DataModels.Products;
using _3M.DataModels.Sales;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3M.DbContexts
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<ShoppingItem> ShoppingItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyConfiguration());
            modelBuilder.ApplyConfiguration(new BillConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ShoppingItemConfiguration());
            modelBuilder.ApplyConfiguration(new UserProfileConfiguration());
        }


    }
}
