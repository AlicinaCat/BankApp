using BankApp.App.Accounts.Queries;
using BankApp.App.Customers.Queries;
using BankApp.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.App.ViewModels
{
    public class IndexViewModel
    {
        private readonly BankAppDataContext context;
        private AccountQueriesHandler Account_Queries;
        private CustomerQueriesHandler Customer_Queries;

        public int TotalCustomers { get; set; }
        public int TotalAccounts { get; set; }
        public decimal TotalBalance { get; set; }

        public IndexViewModel()
        {
            this.context = new BankAppDataContext();
            this.Account_Queries = new AccountQueriesHandler(context);
            this.Customer_Queries = new CustomerQueriesHandler(context);
            this.TotalCustomers = Customer_Queries.GetCustomers().Count;
            this.TotalAccounts = Account_Queries.GetAllAccounts().Count;
            this.TotalBalance = Account_Queries.CalculateTotalBalance();       }           
    }
}