using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.App.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BankApp.Domain;

namespace BankApp.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<Domain.User> userManager;
        private readonly SignInManager<Domain.User> signInManager;

        public UserController(UserManager<Domain.User> userManager, SignInManager<Domain.User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserViewModel model)
        {

            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "UserName/Password does not match!");
                }
            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new Domain.User { UserName = viewModel.Email };
                var result = await userManager.CreateAsync(user, viewModel.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                
                // Error message
            }

            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Login", "User");
        }
    }
}