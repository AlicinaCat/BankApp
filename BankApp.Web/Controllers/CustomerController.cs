using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.App.ViewModels;
using BankApp.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Web.Controllers
{
    [Authorize]
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

            ViewData["TotalResults"] = customers.ToList().Count;

            int pageSize = 50;
            return View(await PaginatedList<Domain.Customer>.CreateAsync(customers, pageNumber ?? 1, pageSize));
        }

        public IActionResult CreateNewCustomer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateNewCustomer(NewCustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                int id = model.CreateNewCustomer();

                return RedirectToAction("Profile", new { id = id });
            }

            return View();
        }

        public IActionResult EditCustomer(int id)
        {
            EditCustomerViewModel model = new EditCustomerViewModel(id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCustomer(EditCustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.SaveChanges();
                return RedirectToAction("Profile", new { id = model.Customer.CustomerId });
            }

            TempData["error"] = "Something went wrong. Please check that all the fields are correct and try again.";

            return View(model);
        }
    }
}

