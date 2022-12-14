using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Dto
{
    public class Order
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
