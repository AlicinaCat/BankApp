using System;
using System.Collections.Generic;
using System.Linq;
using BankApp.Data;
using BankApp.Domain;

namespace BankApp.App.Accounts.Queries
{
    public class Customer_Queries
    {
        private readonly BankAppDataContext context;

        public Customer_Queries(BankAppDataContext context)
        {
            this.context = context;
        }

        public List<Customer> GetCustomers()
        {
            return context.Customers.ToList();
        }
    }
}
