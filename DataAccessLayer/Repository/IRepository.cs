using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Repository
{
    public interface IRepository
    {
        IQueryable<Product> Products { get; }
        IQueryable<Cart> Carts { get; }
        public Product GetProductById(int id);
        public Cart GetCartAndLoadById(int id);
        public Cart GetCartById(int id);
        public decimal GetTotalPriceCart(int CartId);

        public void AddCartProduct(CartProduct product);
        public void DeleteCartProduct(CartProduct product);

        public void seed();
        public void addCart();
    }
}
