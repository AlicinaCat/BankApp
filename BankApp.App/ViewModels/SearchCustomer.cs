using BankApp.App.Accounts.Queries;
using BankApp.App.Customers.Queries;
using BankApp.Data;
using BankApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankApp.App.ViewModels
{
    public class SearchCustomer
    {
        private readonly BankAppDataContext context;
        private CustomerQueriesHandler customer_queries;

        public string UserInput { get; set; }
        public IQueryable<Customer> SearchResults { get; set; }

        public SearchCustomer()
        {
            this.context = new BankAppDataContext();
            this.customer_queries = new CustomerQueriesHandler(context);
        }

        public IQueryable<Customer> GetResults()
        {
            return SearchResults = customer_queries.SearchCustomers(UserInput);
        }
    }
}
