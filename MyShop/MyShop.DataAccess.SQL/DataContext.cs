using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.SQL
{
    public class DataContext : DbContext
    {
        //the first thing is to create a constructor so that we capture & passing that connection string that the base class is expecting
        public DataContext()
            :base("DefaultConnection") {

        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        //store the models in the database, Lecture 73,5:30
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        //lecture 82, 04:06
        public DbSet<Customer> Customers { get; set; }
        //lecture 83, 07:00
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

    }
}
