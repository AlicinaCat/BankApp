using System;
using System.Collections.Generic;
using System.Linq;
using BankApp.Data;
using BankApp.Domain;

namespace BankApp.App.Accounts.Queries
{
    public class GetAllCustomers
    {
        private readonly BankAppDataContext context;

        public GetAllCustomers(BankAppDataContext context)
        {
            this.context = context;
        }

        public List<Customer> Get()
        {
            return context.Customers.ToList();
        }
    }
}
