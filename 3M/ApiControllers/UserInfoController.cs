using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _3M.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _3M.ApiControllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles =RoleConstants.Admin)]
    public class UserInfoController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return Json(new
            {
                Name = User.Identity.Name,
                Claims = User.Claims.Select(i => new { i.Type, i.Value })

            });
        }
    }
}