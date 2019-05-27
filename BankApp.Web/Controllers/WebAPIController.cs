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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BankApp.Web.Controllers
{
    [Route("api/")]
    [ApiController]
    public class WebAPIController : ControllerBase
    {
        private AccountQueriesHandler accountQueriesHandler;
        private TransactionQueryHandler transactionQueryHandler;
        private CustomerQueriesHandler customerQueriesHandler;

        public WebAPIController()
        {
            accountQueriesHandler = new AccountQueriesHandler();
            transactionQueryHandler = new TransactionQueryHandler();
            customerQueriesHandler = new CustomerQueriesHandler();
        }

        [HttpGet()]
        [Route("accounts/")]
        public ActionResult<IEnumerable<Domain.Account>> Get()
        {
            return accountQueriesHandler.GetAllAccounts();
        }

        [HttpGet("accounts/{id}/{page}")]
        public ActionResult<IEnumerable<Transaction>> GetById(int id, int page)
        {
            return transactionQueryHandler.GetTransactionsByAccount(id, page);
        }

        [HttpGet("me")]
        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult/*<Domain.Customer>*/ GetCustomerProfile()
        {
            var idClaim = User.Claims.FirstOrDefault(x => x.Type.Equals("id", StringComparison.InvariantCultureIgnoreCase));

            if (idClaim != null)
            {
                return Ok(customerQueriesHandler.GetCustomer(int.Parse(idClaim.Value)));
            }
            return BadRequest("No claim");
            //return customerQueriesHandler.GetCustomer(id);
        }
    }
}