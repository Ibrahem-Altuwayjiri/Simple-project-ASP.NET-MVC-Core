using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public List<Product> Products { get; set; } = new();
        public List<CartProduct> CartProducts { get; set; } = new();
    }
}
