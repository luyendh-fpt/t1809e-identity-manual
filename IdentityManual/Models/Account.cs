using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityManual.Models
{
    public class Account: IdentityUser
    {
        public Account()
        {
            
        }

        public Account(AccountRegisterModel  accountRegisterModel)
        {
            this.UserName = accountRegisterModel.Username;
            this.Email = accountRegisterModel.Email;
        }
        public string RollNumber { get; set; }
        public string Address { get; set; }
    }
}