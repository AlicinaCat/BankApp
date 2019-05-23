using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BankApp.App.ViewModels;
using BankApp.App.Accounts.Commands;

namespace BankApp.Web.Controllers
{
    public class AccountController : Controller
    {
        AccountCommandHandler accountHandler;

        public AccountController()
        {
            accountHandler = new AccountCommandHandler();
        }

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

        public IActionResult Deposit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Deposit(int accountId, decimal amount)
        {
            accountHandler.Deposit(accountId, amount);

            return View();
        }

        public IActionResult Withdraw()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Withdraw(int accountId, decimal amount)
        {
            accountHandler.Withdraw(accountId, amount);
            return View();
        }

        public IActionResult Transfer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Transfer(int accountFromId, int accountToId, decimal amount)
        {
            accountHandler.Transfer(accountFromId, accountToId, amount);

            return View();
        }
    }
}