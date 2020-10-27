using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace In_Home.Areas.Customers.Controllers
{
    [Authorize]
    [Area("Customers")]
    public class CustomersController : Controller
    {
        public IActionResult Customers()
        {
            return View();
        }
    }
}
