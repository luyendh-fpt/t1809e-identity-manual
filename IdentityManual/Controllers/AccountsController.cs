using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using IdentityManual.Data;
using IdentityManual.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;


namespace IdentityManual.Controllers
{
    public class AccountsController : Controller
    {
        private IdentityManualContext db;
        private UserManager<Account> userManager;

        public AccountsController()
        {
            db = new IdentityManualContext();
            userManager = new UserManager<Account>(new UserStore<Account>(db));
        }

        // GET: Accounts
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            //roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(db));
            //roleManager.Create(new ApplicationRole("Admin"));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAccount(AccountRegisterModel accountViewModel)
        {
            if (ModelState.IsValid)
            {
                var account = new Account(accountViewModel);
                var result = await userManager.CreateAsync(account, accountViewModel.Password);
                if (result.Succeeded)
                {
                    // nên đăng nhập luôn.
                    TempData["message"] = "Register success!";
                    return Redirect("/Home");
                }
            }
            return View("Register", accountViewModel);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            var account = userManager.Find(loginViewModel.Username, loginViewModel.Password);
            if (account != null)
            {
                var ident = userManager.CreateIdentity(account, DefaultAuthenticationTypes.ApplicationCookie);
                //use the instance that has been created. 
                HttpContext.GetOwinContext().Authentication.SignIn( new AuthenticationProperties { IsPersistent = false }, ident);
                TempData["message"] = "Login success!";
                return Redirect("/Home");
            }
            return Redirect("/Accounts/Login");
        }

        public ActionResult Logout()
        {
            TempData["message"] = "Logout success!";
            HttpContext.GetOwinContext().Authentication.SignOut();
            return Redirect("/Home");
        }

    }
}