using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using NATLIB.Models;
using Owin;

[assembly: OwinStartup(typeof(NATLIB.Startup))]

namespace NATLIB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            ConfigureAuth(app);
            CreateRoles();
        }

        public void CreateRoles()
        {
            using (var context = new ApplicationDbContext())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                if (!roleManager.RoleExists("Administrator"))
                {
                    roleManager.Create(new IdentityRole { Name = "Administrator" });

                    var user = new ApplicationUser {
                        UserName = "admin@natlib.com",
                        Email = "admin@natlib.com" };

                    string userPWD = "vQ6u7bu2!";

                    var createUser = userManager.Create(user, userPWD);

                    //Add default User to Role Admin   
                    if (createUser.Succeeded)
                    {
                        userManager.AddToRole(user.Id, "Administrator");
                    }
                }

                if (!roleManager.RoleExists("Librarian"))
                {
                    roleManager.Create(new IdentityRole { Name = "Librarian" });
                }

                if (!roleManager.RoleExists("Local User"))
                {
                    roleManager.Create(new IdentityRole { Name = "Local User" });
                }

                if (!roleManager.RoleExists("Foreign User"))
                {
                    roleManager.Create(new IdentityRole { Name = "Foreign User" });
                }
            }
        }
    }
}
