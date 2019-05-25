using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.App.ViewModels;
using BankApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Web.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<Domain.User> UserManager;
        private readonly BankAppDataContext context;

        public AdminController(UserManager<Domain.User> userManager)
        {
            this.UserManager = userManager;
            this.context = new BankAppDataContext();
        }

        public async Task<IActionResult> ManageUsers()
        {
            var users = UserManager.Users;

            var model = new ManageUsersViewModel()
            {
                UsersWithRoles = new List<UserWithRoles>()
            };

            foreach (var user in users)
            {
                var roles = await UserManager.GetRolesAsync(user);

                model.UsersWithRoles.Add(new UserWithRoles()
                {
                    User = user,
                    IsAdmin = roles.SingleOrDefault(r => r == "Admin") != null
                });
            }


            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageUsers(bool isAdmin, string userId)
        {
            //if (ModelState.IsValid)
            //{
            Domain.User user = await UserManager.FindByIdAsync(userId);

            if (isAdmin)
            {
                if (await UserManager.IsInRoleAsync(user, "Admin"))
                {
                    await UserManager.RemoveFromRoleAsync(user, "Admin");
                }
                else
                {
                    await UserManager.AddToRoleAsync(user, "Admin");
                }
            }
            else
            {
                if (await UserManager.IsInRoleAsync(user, "Admin"))
                {
                    await UserManager.RemoveFromRoleAsync(user, "Admin");
                }
                else
                {
                    await UserManager.AddToRoleAsync(user, "Admin");
                }

            }
            //}

            return Ok();
        }
    }
}