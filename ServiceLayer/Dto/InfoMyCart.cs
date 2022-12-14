using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Dto
{
    public class InfoMyCart
    {
        public IEnumerable<ViewMyProduct> viewMyProduct { get; set; }
        public decimal TotalPriceCart { get; set; }
    }
}
