using BankApp.App.Accounts.Queries;
using BankApp.App.ViewModels;
using BankApp.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.Web.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        public HeaderViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {   
            return View();
        }
    }
}
