using BankApp.App.Accounts.Queries;
using BankApp.App.Customers.Queries;
using BankApp.App.Dispositions.Queries;
using BankApp.Data;
using BankApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankApp.App.ViewModels
{
    public class AccountViewModel
    {
        private readonly BankAppDataContext context;
        CustomerQueriesHandler customer_queries;
        AccountQueriesHandler account_queries;
        public Customer Customer { get; set; }
        public Account Account { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalNumberOfItems { get; set; }
        public bool CanShowMore { get; set; }
        public IList<Transaction> Transactions { get; set; }

        public AccountViewModel(int accountId, int customerId)
        {
            this.context = new BankAppDataContext();
            this.customer_queries = new CustomerQueriesHandler(context);
            this.account_queries = new AccountQueriesHandler(context);

            Account = account_queries.GetAccountWithTransactions(accountId);
            Customer = customer_queries.GetCustomer(customerId);
        }

        public List<Transaction> GetTransactionPage(int take, int skip)
        {
            var transactions = Account.Transactions.OrderByDescending(t => t.Date).Skip(skip).Take(take);
            return transactions.ToList();
        }
    }
}
