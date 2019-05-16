using BankApp.App.Accounts.Queries;
using BankApp.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.App.ViewModels
{
    public class IndexViewModel
    {
        private readonly BankAppDataContext context;
        private Account_Queries Account_Queries;
        private Customer_Queries Customer_Queries;

        public int TotalCustomers { get; set; }

        public IndexViewModel()
        {
            this.context = new BankAppDataContext();
            this.Account_Queries = new Account_Queries(context);
            this.Customer_Queries = new Customer_Queries(context);
            this.TotalCustomers = Customer_Queries.GetCustomers().Count;
        }
            
    }
}
