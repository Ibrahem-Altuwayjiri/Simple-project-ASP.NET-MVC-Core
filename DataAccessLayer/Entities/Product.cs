using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; } = String.Empty;

        public string Description { get; set; } = String.Empty;

        public decimal Price { get; set; }
        public string Category { get; set; } = String.Empty;
        public List<Cart> Carts { get; set; } = new();
        public List<CartProduct> CartProducts { get; set; } = new();
    }
}
