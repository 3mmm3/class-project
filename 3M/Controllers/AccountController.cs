using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _3M.DataModels;
using _3M.ViewModels;
using _3M.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _3M.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


       

        public IActionResult Login()
        {
            var model = new SignInViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(SignInViewModel model, string returnUrl)
        {
            if(!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByNameAsync(model.UserName);
            if(user != null)
            {
                await _signInManager.SignOutAsync();
                var result = await _signInManager.PasswordSignInAsync(user,
                    model.Password, false, false);
                if(result.Succeeded)
                {
                    if (string.IsNullOrEmpty(returnUrl))
                        returnUrl = "/";
                    return LocalRedirect(returnUrl);
                    

                }
               
            }
            ModelState.AddModelError("", "نام کاربری یا رمز عبور اشتباه است");
            return View(model);

        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/");
        }

        [HttpGet]
        [Authorize]
        public IActionResult ChangePAssword()
        {
            var model = new ChangePasswordViewModel();
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);


            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if(result.Succeeded)
            return Redirect("/");
            else
            {
                ModelState.AddModelError("", "رمز عبور فعلی شما صحیح نمی باشد");
                return View(model);
            }
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}