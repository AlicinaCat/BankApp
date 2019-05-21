using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.App.ViewModels;
using BankApp.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Web.Controllers
{
    public class CustomerController : Controller
    {
        

        public IActionResult Profile(int id)
        {
            CustomerProfile model = new CustomerProfile(id);

            return View(model);
        }

        public async Task<IActionResult> SearchCustomers(
            string currentFilter,
            string searchString,
            int? pageNumber)
        {

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            SearchCustomer model = new SearchCustomer();

            model.UserInput = searchString;
            var customers = model.GetResults();

            if (!String.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(s => s.Surname.Contains(searchString)
                                       || s.Givenname.Contains(searchString));
            }

            int pageSize = 50;
            return View(await PaginatedList<Domain.Customer>.CreateAsync(customers.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

    }
}

