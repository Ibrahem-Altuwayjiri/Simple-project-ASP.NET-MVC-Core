using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class CartProduct
    {
        public int CartId { get; set; } = 1;
        public Cart Cart { get; set; }
        public int ProductId { get; set; }
        public Product  Product { get; set; }
        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
