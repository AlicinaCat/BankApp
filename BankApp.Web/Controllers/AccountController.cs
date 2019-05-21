﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BankApp.App.ViewModels;

namespace BankApp.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Overview(int accountId, int customerId)
        {
            AccountViewModel model = new AccountViewModel(accountId, customerId);

            return View(model);
        }
    }
}