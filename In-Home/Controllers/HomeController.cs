using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using In_Home.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using In_Home.Areas.Users.Models;

namespace In_Home.Controllers
{
    public class HomeController : Controller
    {
        //IServiceProvider _serviceProvider;
        private static InputModelLogin _model;
        public HomeController(IServiceProvider serviceProvider)
        {
            //_serviceProvider = serviceProvider;
        }

        public async Task<IActionResult> Index()
        {
            //await CreateRolesAsync(_serviceProvider);
            if(_model != null)
            {
                return View(_model);
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Index(InputModelLogin model)
        {
            _model = model;
            if(ModelState.IsValid)
            {
                return View();
            }
            else
            {
                foreach(var modelState in ModelState.Values)
                {
                    foreach(var error in modelState.Errors)
                    {
                        _model.ErrorMessage = error.ErrorMessage;
                    }
                }
                return Redirect("/");
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private async Task CreateRolesAsync(IServiceProvider serviceProvider) 
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            String[] rolesName = { "Admin", "User" };
            foreach (var item in rolesName)
            {
                var roleExist = await roleManager.RoleExistsAsync(item);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(item));
                }
            }
        }
    }
}
