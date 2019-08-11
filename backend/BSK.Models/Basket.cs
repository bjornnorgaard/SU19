using System.Collections.Generic;

namespace BSK.Models
{
    public class Basket
    {
        public int UserId { get; }
        public List<Product> Products { get; }

        public Basket(int userId)
        {
            UserId = userId;
            Products = new List<Product>();
        }

        public void Add(Product product)
        {
            Products.Add(product);
        }

        public void Remove(Product product)
        {
            Products.Remove(product);
        } 
    }
}