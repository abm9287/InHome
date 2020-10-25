using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace In_Home.Areas.Users.Controllers
{
    public class UsersController : Controller
    {
        [Area("Users")]
        public IActionResult Users()
        {
            return View();
        }
    }
}
