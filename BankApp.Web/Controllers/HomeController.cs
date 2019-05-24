using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BankApp.Domain;
using BankApp.App.ViewModels;
using BankApp.App;
using BankApp.App.Accounts.Queries;
using Microsoft.AspNetCore.Authorization;

namespace BankApp.Web.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        // GET: /<controller>/
        public IActionResult Index()
        {
            IndexViewModel model = new IndexViewModel();

            return View(model);
        }
    }
}
