using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.MvcWEBUI.Entity;
using Ecommerce.MvcWEBUI.Models;

namespace Ecommerce.MvcWEBUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class OrderController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Order
        public ActionResult Index()
        {
            var orders = db.Orders
                .Select(i => new AdminOrderModel()
                {
                    Id = i.Id,
                    OrderDate = i.OrderDate,
                    OrderNumber = i.OrderNumber,
                    OrderState = i.OrderState,
                    Total = i.Total,
                    Count = i.OrderLines.Count


                }).OrderByDescending(i => i.OrderDate).ToList();
            return View(orders);
        }

        public ActionResult Details(int id)
        {
            var entity = db.Orders
                .Where(i => i.Id == id)
                .Select(i => new OrderDetailsModel()
                {
                    OrderId = i.Id,
                    OrderNumber = i.OrderNumber,
                    OrderDate = i.OrderDate,
                    OrderState = i.OrderState,
                    Total = i.Total,
                    AddressTitle = i.AddressTitle,
                    Address = i.Address,
                    City = i.City,
                    State = i.State,
                    Zipcode = i.Zipcode,
                    OrderLines = i.OrderLines.Select(j => new OrderLineModel()
                    {
                        ProductId = j.ProductId,
                        Price = j.Price,
                        ProductName = j.Product.Name.Length > 50 ? j.Product.Name.Substring(0, 25) + "..." : j.Product.Name,
                        Image = j.Product.Image,
                        Quantity = j.Quantity,

                    }).ToList()
                }).FirstOrDefault();
            return View(entity);
        }
    }
}