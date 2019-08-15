using System.Collections.Generic;
using BSK.Models.Database;

namespace BSK.Repository
{
    public interface IBskContext
    {
        List<User> Users { get; set; }
        List<Basket> Baskets { get; set; }
        List<Product> Products { get; set; }
    }
}