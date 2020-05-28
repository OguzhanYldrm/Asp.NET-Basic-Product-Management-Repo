using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.MvcWEBUI.Models;
using Ecommerce.MvcWEBUI.Entity;

namespace Ecommerce.MvcWEBUI.Controllers
{
    public class HomeController : Controller
    {
        DataContext _context = new DataContext();

        // GET: Home
        public ActionResult Index()
        {
            var model = _context.Products
                                        .Where(i => i.IsHome && i.IsApproved)
                                        .Select(i => new ProductModel()
                                        {
                                            Id = i.Id,
                                            Name = i.Name.Length > 50 ? i.Name.Substring(0, 47) + "..." : i.Name,
                                            Description = i.Description.Length>50?i.Description.Substring(0,47)+"...":i.Description,
                                            Price = i.Price,
                                            Stock = i.Stock,
                                            Image = !string.IsNullOrEmpty(i.Image) ? i.Image : "null.jpg",
                                            CategoryId = i.CategoryId
                                        }).ToList();

            return View(model);
        }

        public ActionResult Details(int id)
        {
            return View(_context.Products.Where(i => i.Id == id).FirstOrDefault());
        }

        public ActionResult List(int? id)
        {
            var model = _context.Products
                .Where(i => i.IsApproved )
                .Select(i => new ProductModel()
                {
                    Id = i.Id,
                    Name = i.Name.Length > 50 ? i.Name.Substring(0, 47) + "..." : i.Name,
                    Description = i.Description.Length > 50 ? i.Description.Substring(0, 47) + "..." : i.Description,
                    Price = i.Price,
                    Stock = i.Stock,
                    Image = !string.IsNullOrEmpty(i.Image) ? i.Image : "null.jpg",
                    CategoryId = i.CategoryId
                }).AsQueryable();

            if (id != null)
            {
                model = model.Where(i => i.CategoryId == id);
            }

            return View(model.ToList());
        }

        [ChildActionOnly]
        public PartialViewResult GetCategories()
        {
            return PartialView(_context.Categories.ToList());
        }
    }
}