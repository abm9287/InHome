using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using In_Home.Areas.Customers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace In_Home.Areas.Customers.Pages.Account
{
    public class RegisterModel : PageModel
    {
        public void OnGet()
        {
            Input = new InputModel
            {

            };
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel : InputModelRegister
        {
            public IFormFile AvatarImage { get; set; }
        }
    }
}
