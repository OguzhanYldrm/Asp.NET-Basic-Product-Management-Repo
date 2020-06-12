using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.MvcWEBUI.Entity;
using Ecommerce.MvcWEBUI.Models;

namespace Ecommerce.MvcWEBUI.Controllers
{
    public class CartController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Cart
        public ActionResult Index()
        {


            return View(GetCart());
        }

        public ActionResult AddToCart(int Id)
        {
            var product = db.Products.FirstOrDefault(i => i.Id == Id);

            if (product != null)
            {
                GetCart().AddProduct(product, 1);
            }

            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int Id)
        {
            var product = db.Products.FirstOrDefault(i => i.Id == Id);

            if (product != null)
            {
                GetCart().DeleteProduct(product);
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Checkout()
        {

            return View(new ShippingDetails());
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(ShippingDetails details)
        {
            var cart = GetCart();
            if (cart.CartLines.Count == 0)
            {
                ModelState.AddModelError("CartEmpty", "Your cart is empty!");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    // Saving order to db
                    // reset cart
                    SaveOrder(cart, details);


                    cart.Clear();

                    return View("Completed");
                }
                else
                {
                    return View(details);
                }
            }
            return View();
        }

        private void SaveOrder(Cart cart, ShippingDetails details)
        {
            var order = new Order();

            order.OrderNumber = "Prod" + (new Random()).Next(111111, 999999).ToString();
            order.Total = cart.Total();
            order.OrderDate = DateTime.Now;
            order.OrderState = EnumOrderState.Waiting;

            order.UserName = User.Identity.Name;

            order.AddressTitle = details.AddressTitle;
            order.Address = details.Address;
            order.City = details.City;
            order.State = details.State;
            order.Zipcode = details.Zipcode;
            order.OrderLines = new List<OrderLine>();

            foreach (var pr in cart.CartLines)
            {
                var orderline = new OrderLine();
                orderline.Quantity = pr.Quantity;
                orderline.Price = pr.Quantity * pr.Product.Price;
                orderline.ProductId = pr.Product.Id;

                order.OrderLines.Add(orderline);
            }

            db.Orders.Add(order);
            db.SaveChanges();
        }

        public Cart GetCart()
        {
            var cart = (Cart)Session["Cart"];

            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }

            return cart;
        }


        public PartialViewResult Summary()
        {
            return PartialView(GetCart());
        }
    }
}