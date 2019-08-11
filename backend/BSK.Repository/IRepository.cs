using System.Collections.Generic;
using BSK.Models;

namespace BSK.Repository
{
    public interface IRepository
    {
        List<User> Users { get; set; }
        List<Basket> Baskets { get; set; }
        List<Product> Products { get; set; }
    }
}