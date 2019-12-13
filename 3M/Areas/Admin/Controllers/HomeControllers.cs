using _3M.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3M.Areas.Admin.Controllers
{
    public class HomeControllers : Controller
    {
        [Area("Admin")]
        [Authorize(Roles = RoleConstants.Admin)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
