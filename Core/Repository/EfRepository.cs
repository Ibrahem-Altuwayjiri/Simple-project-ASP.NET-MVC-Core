using DataAccessLayer.Database;
using DomainLayer.Entities;
using DomainLayer.Repository;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Dto;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class EfRepository : IRepository
    {
        private StoreDbContext context;

        public EfRepository(StoreDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Product> Products => context.Products;
        public IQueryable<Cart> Carts => context.Carts.Include(c => c.CartProducts);



        public void AddCartProduct(CartProduct product)
        {
            var cart = GetCartAndLoadById(product.CartId);

            var existingCartProduct = cart.CartProducts.SingleOrDefault(cp => cp.CartId == product.CartId && cp.ProductId == product.ProductId);
            //var existingCartProduct = context.Carts.Where(c => c.CartProducts
            //.Single(cp => cp.CartId == product.CartId )
            //);
            //.cartProducts.SingleOrDefault(cp => cp.ProductId == product.ProductId);
            if (existingCartProduct is null)
            {


                cart.CartProducts.Add(new CartProduct { CartId = product.CartId, ProductId = product.ProductId, Quantity = product.Quantity, UnitPrice = product.UnitPrice });
            }
            else
            {
                existingCartProduct.Quantity = product.Quantity;
                context.Set<CartProduct>().Update(existingCartProduct);
            }
            context.SaveChanges();
        }

        public void DeleteCartProduct(CartProduct product)
        {
            var cart = GetCartAndLoadById(product.CartId);
            var existingCartProduct = cart.CartProducts.SingleOrDefault(cp => cp.CartId == product.CartId && cp.ProductId == product.ProductId);

            if (existingCartProduct is not null)
            {
                context.Set<CartProduct>().Remove(existingCartProduct);
                context.SaveChanges();
            }

        }

        public Product GetProductById(int id)
        {
            var product = context.Products.Where(p => p.ProductId == id).FirstOrDefault();
            if (product != null)
            {
                return product;
            }
            else
                return null;

        }
        public Cart GetCartById(int id)
        {
            var cart = context.Carts.Find(id);
            if (cart != null)
            {
                return cart;
            }
            else
                return null;
        }
        public Cart GetCartAndLoadById(int id)
        {
            var cart = context.Carts.Find(id);

            if (cart != null)
            {
                context.Entry(cart).Collection(cp => cp.CartProducts).Load();
                return cart;
            }
            else
                return null;
        }

        public decimal GetTotalPriceCart(int CartId)
        {
            var cart = GetCartAndLoadById(CartId);
            decimal totalPriceCart = 0;
            foreach (var product in cart.CartProducts)
            {
                totalPriceCart += product.TotalPrice;
            }
            return totalPriceCart;
        }


        public void seed()
        {


            if (!context.Products.Any())
            {
                context.Products.AddRange(
                new Product
                {
                    Name = "Kayak",
                    Description = "A boat for one person",
                    Category = "Watersports",
                    Price = 275
                },
                new Product
                {
                    Name = "Lifejacket",
                    Description = "Protective and fashionable",
                    Category = "Watersports",
                    Price = 48.95m
                },
                new Product
                {
                    Name = "Soccer Ball",
                    Description = "FIFA-approved size and weight",
                    Category = "Soccer",
                    Price = 19.50m
                },
                new Product
                {
                    Name = "Corner Flags",
                    Description = "Give your playing field a professional touch",
                    Category = "Soccer",
                    Price = 34.95m
                },
                new Product
                {
                    Name = "Stadium",
                    Description = "Flat-packed 35,000-seat stadium",
                    Category = "Soccer",
                    Price = 79500
                },
                new Product
                {
                    Name = "Thinking Cap",
                    Description = "Improve brain efficiency by 75%",
                    Category = "Chess",
                    Price = 16
                },
                new Product
                {
                    Name = "Unsteady Chair",
                    Description = "Secretly give your opponent a disadvantage",
                    Category = "Chess",
                    Price = 29.95m
                },
                new Product
                {
                    Name = "Human Chess Board",
                    Description = "A fun game for the family",
                    Category = "Chess",
                    Price = 75
                },
                new Product
                {
                    Name = "Bling-Bling King",
                    Description = "Gold-plated, diamond-studded King",
                    Category = "Chess",
                    Price = 1200
                }
             );
                context.SaveChanges();
            }


            //if (context.Carts.Any())
            //{
            //    //eagor loading
            //    var cart = context.Carts.Include(c => c.cartProducts).First();
            //    cart.cartProducts.Clear();
            //    context.SaveChanges();
            //}
        }

        public void addCart()
        {
            if (!context.Carts.Any())
            {
                Cart cart = new Cart();
                context.Carts.Add(cart);
                context.SaveChanges();
            }

        }


    }
}
