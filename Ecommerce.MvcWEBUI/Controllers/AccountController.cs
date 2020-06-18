using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.MvcWEBUI.Entity;
using Ecommerce.MvcWEBUI.Identity;
using Ecommerce.MvcWEBUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace Ecommerce.MvcWEBUI.Controllers
{
    public class AccountController : Controller
    {
        private DataContext db = new DataContext();

        private UserManager<ApplicationUser> _userManager;

        private RoleManager<ApplicationRole> _roleManager;

        public AccountController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            _userManager = new UserManager<ApplicationUser>(userStore);


            var roleStore = new RoleStore<ApplicationRole>(new IdentityDataContext());
            _roleManager = new RoleManager<ApplicationRole>(roleStore);
        }




        [Authorize]
        public ActionResult Index()
        {
            var orders = db.Orders
                .Where(i => i.UserName == User.Identity.Name)
                .Select(i => new UserOrderModel()
                {
                    Id = i.Id,
                    OrderNumber = i.OrderNumber,
                    OrderDate = i.OrderDate,
                    OrderState = i.OrderState,
                    Total = i.Total
                }).OrderByDescending(i => i.OrderDate).ToList();

            return View(orders);
        }

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                //find user
                var user = _userManager.Find(model.Username, model.Password);
                if (user != null)
                {
                    //if user exists
                    //create cookie
                    var authManager = HttpContext.GetOwinContext().Authentication;
                    var identityclaims = _userManager.CreateIdentity(user, "ApplicationCookie");
                    var authProperties = new AuthenticationProperties();
                    authProperties.IsPersistent = model.RememberMe;
                    authManager.SignIn(authProperties, identityclaims);

                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("RegisterUserError", "User Login Error. ");

                }
            }

            return View(model);
        }

        // GET: Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                //Registering user
                ApplicationUser user = new ApplicationUser();
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Email = model.Email;
                user.UserName = model.Username;

                IdentityResult result = _userManager.Create(user, model.Password);

                if (result.Succeeded)
                {
                    if (_roleManager.RoleExists("user"))
                    {
                        //give a role to user
                        _userManager.AddToRole(user.Id, "user");
                    }

                    return RedirectToAction("Login", "Account");

                }
                else
                {
                    ModelState.AddModelError("RegisterUserError", "An Error occurred while creating user. ");
                }
            }

            return View(model);
        }

        // GET: Logout
        [Authorize]
        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();

            return RedirectToAction("Index", "Home");

        }

        [Authorize]
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
                        ProductName = j.Product.Name.Length>50?j.Product.Name.Substring(0,25)+"...":j.Product.Name,
                        Image = j.Product.Image,
                        Quantity = j.Quantity,
                        
                    }).ToList()
                }).FirstOrDefault();

            return View(entity);
        }
    }
}