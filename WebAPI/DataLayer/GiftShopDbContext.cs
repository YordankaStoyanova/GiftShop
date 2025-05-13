using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    internal class GiftShopDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }


        public GiftShopDbContext() : base() { }
        public GiftShopDbContext(DbContextOptions optionsBuilder) : base(optionsBuilder) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured) optionsBuilder.UseSqlite("Data Source=gift_shop.db3");
            base.OnConfiguring(optionsBuilder);
        }
        
    }
}
