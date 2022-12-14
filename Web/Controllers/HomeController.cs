using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ServiceLayer.Dto;
using Microsoft.EntityFrameworkCore.Migrations;
using DomainLayer.Repository;
using DomainLayer.Entities;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _productRepository;

        public HomeController(IRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index(int currentPage = 1)
        {
            //Add some data in database
            _productRepository.addCart();
            _productRepository.seed();

            var numberOfItem = _productRepository.Products.Count();
            int itemPerPage = 5;
            int skipItem = (currentPage - 1) * itemPerPage;
            PagingInfo pageinfo = new PagingInfo
            {
                TotalItems = numberOfItem,
                CurrentPage = currentPage,
                ItemPerPage = itemPerPage,
            };

            var listOfProduct = _productRepository.Products.Skip(skipItem).Take(itemPerPage).ToList();

            ProductView productView = new ProductView
            {
                pagingInfo = pageinfo,
                ListOfProduct = listOfProduct
            };

            return View(productView);
        }
        [HttpPost]
        public IActionResult AddToCart(Order order)
        {
            CartProduct cartProduct = CreateCartProduct(order);
            _productRepository.AddCartProduct(cartProduct);


            string urlName = Request.Headers["Referer"].ToString();
            if (urlName.Contains("ListOfMyProduct"))
            {
                return RedirectToAction("ListOfMyProduct");
            }
            return RedirectToAction("Index");
        }

        public IActionResult DeleteFromCart(Order order)
        {
            CartProduct cartProduct = CreateCartProduct(order);
            _productRepository.DeleteCartProduct(cartProduct);
            return RedirectToAction("ListOfMyProduct");
        }

        public CartProduct CreateCartProduct(Order order)
        {
            var product = _productRepository.GetProductById(order.ProductId);
            var cart = _productRepository.GetCartById(1);
            CartProduct cartProduct = new CartProduct
            {
                CartId = cart.Id,
                ProductId = product.ProductId,
                UnitPrice = product.Price,
                Quantity = order.Quantity
            };
            return cartProduct;
        }

        public IActionResult ListOfMyProduct(int CartId = 1)
        {
            var cart = _productRepository.GetCartAndLoadById(CartId);
            var totalPriceCart = _productRepository.GetTotalPriceCart(CartId);


            var listOfCartProduct = cart.CartProducts
                .Join(
                    _productRepository.Products,
                    cp => cp.ProductId,
                    p => p.ProductId,
                    (cp, product) => new ViewMyProduct
                    {
                        CartId = cp.CartId,
                        ProductId = cp.ProductId,
                        ProductName = product.Name,
                        ProductDescription = product.Description,
                        Quantity = cp.Quantity,
                        UnitPrice = cp.UnitPrice,
                        TotalPrice = cp.TotalPrice
                    }
                );
            InfoMyCart infoMyCart = new InfoMyCart
            {
                viewMyProduct = listOfCartProduct,
                TotalPriceCart = totalPriceCart,
            };


            return View(infoMyCart);
        }
    }
}