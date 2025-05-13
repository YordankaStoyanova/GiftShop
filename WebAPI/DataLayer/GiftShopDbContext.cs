using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class GiftShopDbContext:DbContext
    {
        internal DbSet<User> Users { get; set; }
        internal DbSet<Product> Products { get; set; }
        internal DbSet<Order> Orders { get; set; }
        internal DbSet<Feedback> Feedbacks { get; set; }


        public GiftShopDbContext() : base() { }
        public GiftShopDbContext(DbContextOptions optionsBuilder) : base(optionsBuilder) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured) optionsBuilder.UseSqlite("Data Source=gift_shop.db3");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(u =>
            {
                u.HasIndex(u => u.Email);
                u.HasMany(u => u.Orders)
                .WithOne(o => o.User);
            });
            modelBuilder.Entity<Order>()
                .HasOne(u => u.User)
                .WithMany(u => u.Orders);
            base.OnModelCreating(modelBuilder);
        }
    }
}
