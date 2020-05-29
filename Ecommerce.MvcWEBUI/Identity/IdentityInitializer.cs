using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Ecommerce.MvcWEBUI.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ecommerce.MvcWEBUI.Identity
{
    public class IdentityInitializer : CreateDatabaseIfNotExists<IdentityDataContext>
    {
        protected override void Seed(IdentityDataContext context)
        {
            //Roles
            if (!context.Roles.Any(i => i.Name == "admin"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);

                var role = new ApplicationRole("admin", "Administrator Role") { Name = "admin", Description = "admin role" }; ;
                manager.Create(role);
            }

            if (!context.Roles.Any(i => i.Name == "user"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);

                var role = new ApplicationRole("user", "User Role") { Name = "user",Description = "user role"};
                manager.Create(role);
            }



            //Users
            if (!context.Roles.Any(i => i.Name == "OğuzhanYıldırım"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser()
                { Name = "Oğuzhan", Surname = "Yıldırım", UserName = "drojan35", Email = "info.oguzhanyildirim@gmail.com" };
                manager.Create(user, "12345678");
                manager.AddToRole(user.Id, "admin");
                manager.AddToRole(user.Id, "user");
            }

            if (!context.Roles.Any(i => i.Name == "KemalYıldırım"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser()
                { Name = "Kemal", Surname = "Yıldırım", UserName = "kemal35", Email = "keml.yl@gmail.com" };
                manager.Create(user, "12345678");
                manager.AddToRole(user.Id, "user");
            }

            base.Seed(context);
        }
    }
}