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
    public class SearchCustomersViewComponent : ViewComponent
    {
        private readonly BankAppDataContext context;
        private Customer_Queries customer_queries;

        public SearchCustomersViewComponent()
        {
            this.context = new BankAppDataContext();
            this.customer_queries = new Customer_Queries(context);
        }

        public async Task<IViewComponentResult> InvokeAsync(string userInput)
        {
            var model = new SearchCustomer();
            
            return View(model);
        }
    }
}
