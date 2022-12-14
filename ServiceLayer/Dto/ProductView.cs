using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Dto
{
    public class ProductView
    {
        public PagingInfo pagingInfo { get; set; }
        public List<Product> ListOfProduct { get; set; }
        
    }
}
