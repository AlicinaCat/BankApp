using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BankApp.App.ViewModels;
using BankApp.App.Accounts.Commands;
using Microsoft.AspNetCore.Authorization;

namespace BankApp.Web.Controllers
{
    [Authorize]
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

            const int pageSize = 20;

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
            if (ModelState.IsValid)
            {
                int result = accountHandler.Deposit(accountId, amount);

                if (result == 1)
                {
                    TempData["success"] = "Deposit executed successfully.";

                    return View();
                }
            }

            TempData["error"] = "Deposit was unsuccessful. Please check that amount and account Id are correct.";


            return View();
        }

        public IActionResult Withdraw()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Withdraw(int accountId, decimal amount)
        {
            if (ModelState.IsValid)
            {
                int result = accountHandler.Withdraw(accountId, amount);

                if (result == 1)
                {
                    TempData["success"] = "Withdrawal executed successfully.";

                    return View();
                }
            }

            TempData["error"] = "Withdrawal was unsuccessful. Please check that amount and account Id are correct.";

            return View();
        }

        public IActionResult Transfer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Transfer(int accountFromId, int accountToId, decimal amount)
        {
            if (ModelState.IsValid)
            {
                int result = accountHandler.Transfer(accountFromId, accountToId, amount);

                if (result == 1)
                {
                    TempData["success"] = "Transfer executed successfully.";

                    return View();
                }
            }

            TempData["error"] = "Transfer was unsuccessful. Please check that amount and account Id are correct.";
            

            return View();
        }

        public IActionResult ApplyInterest()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ApplyInterest(int accountId, double rate, DateTime lastTimeApplied)
        {
            if (ModelState.IsValid)
            {
                int result = accountHandler.ApplyInterest(accountId, rate, lastTimeApplied);

                if (result == 1)
                {
                    TempData["success"] = "Operation executed successfully.";

                    return View();
                }
            }

            TempData["error"] = "Operation was unsuccessful. Please check that rate and account Id are correct.";


            return View();
        }
    }
}