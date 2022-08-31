using BoschSpot.App.ViewModels;
using BoschSpot.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BoschSpot.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepo _accountRepo;

        public AccountController(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var rvm = new RegisterViewModel();
            return View(rvm);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel rvm) 
        {
            _accountRepo.CreateAccount(rvm);
            await Login(new LoginViewModel { Username = rvm.Username, Password = rvm.Password });
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login() 
        {
            var lvm = new LoginViewModel();
            return View(lvm);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel lvm) 
        {
            var user = _accountRepo.CheckAuthentication(lvm);
            if (user != null) 
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, lvm.Username),
                    new Claim(ClaimTypes.Sid, user.ID)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("Index", "Home");
            }
            return View();  
        }

        public  async Task<IActionResult> Logout() 
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
