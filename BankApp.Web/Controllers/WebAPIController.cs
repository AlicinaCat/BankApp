using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApp.App.Accounts.Queries;
using BankApp.App.Customers.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankApp.App.Transactions.Queries;
using BankApp.Domain;

namespace BankApp.Web.Controllers
{
    [Route("api/accounts/")]
    [ApiController]
    public class WebAPIController : ControllerBase
    {
        private AccountQueriesHandler accountQueriesHandler;
        private TransactionQueryHandler transactionQueryHandler;

        public WebAPIController()
        {
            accountQueriesHandler = new AccountQueriesHandler();
            transactionQueryHandler = new TransactionQueryHandler();
        }

        [HttpGet()]
        public ActionResult<IEnumerable<Domain.Account>> Get()
        {
            return accountQueriesHandler.GetAllAccounts();
        }

        [HttpGet("{id}/{page}")]
        public ActionResult<IEnumerable<Transaction>> GetById(int id, int page)
        {
            return transactionQueryHandler.GetTransactionsByAccount(id, page);
        }
    }
}