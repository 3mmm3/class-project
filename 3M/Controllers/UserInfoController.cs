using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _3M.Controllers
{
    [Authorize]
    public class UserInfoController : Controller
    {
        public IActionResult Index()
        {
            return Json(new
            {
                Name  = User.Identity.Name,
                Claims = User.Claims.Select(i => new { i.Type, i.Value })

            });
        }
    }
}