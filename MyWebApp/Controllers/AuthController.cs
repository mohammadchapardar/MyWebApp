using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyWebApp.Dto;
using MyWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApp.Controllers
{
    public class AuthController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Signin(SigninViewModel model, string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = "/";
            ViewData["returnUrl"] = returnUrl;
            bool isLogin = false;
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                isLogin = false;
            }
            else
            {
                isLogin = await _userManager.CheckPasswordAsync(user, model.PassWord);
            }
            if (isLogin)
            {
                await _signInManager.SignInAsync(user, true);
                return Redirect(returnUrl);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Signin(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = "/";
            ViewData["returnUrl"] = returnUrl;
            return View();
        }
        public IActionResult Logout(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = "/";
            _signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }
    }
}
