using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using In_Home.Areas.Users.Models;
using In_Home.Data;
using In_Home.Library;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace In_Home.Areas.Users.Pages.Account
{
    public class DetailsModel : PageModel
    {
        //atributos
        private SignInManager<IdentityUser> _signInManager;
        private LUser _user;
        //Método constructor
        public DetailsModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _user = new LUser(userManager, signInManager, roleManager, context);
        }
        public void OnGet(int id)
        {
            var data = _user.getTUsuariosAsync(null, id); 
            if(0 < data.Result.Count)
            {
                Input = new InputModel
                {
                    DataUser = data.Result.ToList().Last(),
                };
            }
        }
        [BindProperty]
       public InputModel Input { get; set; }
        public class InputModel
        {
            public InputModelRegister DataUser { get; set; }
        }
    }
}
