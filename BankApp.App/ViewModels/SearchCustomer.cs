using BankApp.App.Accounts.Queries;
using BankApp.Data;
using BankApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.App.ViewModels
{
    public class SearchCustomer
    {
        private readonly BankAppDataContext context;
        private Customer_Queries customer_queries;

        public string UserInput { get; set; }
        public List<Customer> SearchResults { get; set; }

        public SearchCustomer()
        {
            this.context = new BankAppDataContext();
            this.customer_queries = new Customer_Queries(context);
        }

        public List<Customer> GetResults()
        {
            return SearchResults = customer_queries.SearchCustomers(UserInput);
        }
    }
}
