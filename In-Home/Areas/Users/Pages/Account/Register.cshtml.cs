using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using In_Home.Areas.Users.Models;
using In_Home.Data;
using In_Home.Library;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace In_Home.Areas.Users.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private SignInManager<IdentityUser> _signInManager;
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ApplicationDbContext _context;
        private LUsersRoles _usersRole;
        private static InputModel _dataInput;


        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _usersRole = new LUsersRoles();

        }
        public void OnGet()
        {
            if(_dataInput != null)
            {
                Input = _dataInput;
                Input.rolesLista = _usersRole.getRoles(_roleManager);
                Input.AvatarImage = null;
            }
            else
            {
                Input = new InputModel
                {
                    rolesLista = _usersRole.getRoles(_roleManager)
                };
            }
          
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel : InputModelRegister
        {
            public IFormFile AvatarImage { get; set; }
            [TempData]
            public string ErrorMessage { get; set; }
            public List<SelectListItem> rolesLista { get; set; }
        }
        public async Task<IActionResult> OnPost()
        {
            if(await SaveAsync())
            {
                return Redirect("/Users/Users?are=Users");
            }
            else
            {
                return Redirect("/Users/Register");
            }
        }
        private async Task<bool> SaveAsync()
        {
            _dataInput = Input;
            var valor = false;
            if(ModelState.IsValid)
            {
                var userList = _userManager.Users.Where(u => u.Email.Equals(Input.Email)).ToList();
                if(userList.Count.Equals(0))
                {
                    var strategy = _context.Database.CreateExecutionStrategy();
                }
                else
                {
                    _dataInput.ErrorMessage = $"The  {Input.Email} is registered ";
                    valor = false;
                }
            }
            else
            {
                _dataInput.ErrorMessage = "Select a role";
                valor = false;
            }
            return valor;
        }
    }
}
