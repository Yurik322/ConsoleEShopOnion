using System.Collections.Generic;
using OA_DataAccess.Entities;

namespace OA_DataAccess.EF
{
    /// <summary>
    ///  Data storage.
    /// </summary>
    public class EShopContext
    {
        public EShopContext()
        {
            this.OnModelCreating();
        }

        public List<User> Users { get; set; }
        public List<Product> Products { get; set; }
        public List<Order> Orders { get; set; }

        /// <summary>
        /// Method for creating data.
        /// </summary>
        public void OnModelCreating()
        {
            Users = new List<User>()
            {
                new User { Id = 1, Email = "admin@gmail.com", Role = Roles.Admin },
                new User { Id = 2, Email = "user@gmail.com", Role = Roles.Registered },
                new User { Id = 3, Email = "elonmusk@gmail.com", Role = Roles.Registered },
            };
            Products = new List<Product>()
            {
                new Product { Id = 1, Name = "Samsung s20", Category = "mobile phones", Description = "good for You", Price = 500M },
                new Product { Id = 2, Name = "Iphone 12", Category = "mobile phones", Description = "good for You", Price = 1000M },
                new Product { Id = 3, Name = "Xiaomi", Category = "mobile phones", Description = "good for You", Price = 200M },
            };
            Orders = new List<Order>()
            {
                new Order
                {
                    Id = 1, UserId = 2, ProductId = 1, TotalPrice = 500M, Status = "New"
                }
            };
        }
    }
}
