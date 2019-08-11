using System.Collections.Generic;
using BSK.Models.Database;

namespace BSK.Repository
{
    public class Repository : IRepository
    {
        public List<User> Users { get; set; } = new List<User>();
        public List<Basket> Baskets { get; set; } = new List<Basket>();
        public List<Product> Products { get; set; } = new List<Product>();

        public Repository()
        {
            var users = new List<User>
            {
                new User {Id = 1, Name = "John"},
                new User {Id = 2, Name = "Simone"},
                new User {Id = 3, Name = "Anders"},
                new User {Id = 4, Name = "Karl"},
                new User {Id = 5, Name = "Erik"},
            };
            Users.AddRange(users);

            var products = new List<Product>
            {
                new Product {Id = 1, Name = "Towel"},
                new Product {Id = 2, Name = "Sunscreen"},
                new Product {Id = 3, Name = "Laptop"},
                new Product {Id = 4, Name = "Beef"},
                new Product {Id = 5, Name = "Fridge"},
            };
            Products.AddRange(products);
        }
    }
}
