using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IdentityManual.Data;
using IdentityManual.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityManual.Controllers
{
    public class RolesController : Controller
    {
        private IdentityManualContext db;
        private RoleManager<ApplicationRole> roleManager;
        private UserManager<Account> userManager;

        public RolesController()
        {
            db = new IdentityManualContext();
            roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(db));
            userManager = new UserManager<Account>(new UserStore<Account>(db));
        }
        // GET: Roles
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(string roleName)
        {
            roleManager.CreateAsync(new ApplicationRole(roleName));
            TempData["message"] = "Add role success!";
            return Redirect("/Home");
        }

        public ActionResult AddUserToRole(string username, string roleName)
        {
            var user = db.Users.Where(u => u.UserName == username).FirstOrDefault();
            if (user == null)
            {
                return HttpNotFound();
            }

            if (roleManager.RoleExists(roleName))
            {
                userManager.AddToRole(user.Id, roleName);
                TempData["message"] = "Add role success!";
                return Redirect("/Home");
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }


    }
}