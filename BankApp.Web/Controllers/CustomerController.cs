using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.App.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Web.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Profile(int id)
        {
            CustomerProfile model = new CustomerProfile(id);

            return View(model);
        }
    }
}