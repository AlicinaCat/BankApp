using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BankApp.App.ViewModels;

namespace BankApp.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Overview(int accountId, int customerId, int page = 1)
        {
            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            const int pageSize = 10;

            AccountViewModel model = new AccountViewModel(accountId, customerId);

            var totalNumber = model.Account.Transactions.Count;
            var transactions = model.GetTransactionPage(pageSize, (page - 1) * pageSize);

            model.PageNumber = page;
            model.PageSize = pageSize;
            model.TotalNumberOfItems = totalNumber;
            model.CanShowMore = page * pageSize < totalNumber;
            model.Transactions = transactions;


            if (isAjax)
            {
                return PartialView("_TransactionList", model);
            }
            else
            {
                return View(model);
            };
        }
    }
}