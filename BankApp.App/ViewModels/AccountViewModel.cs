﻿using BankApp.App.Accounts.Queries;
using BankApp.App.Customers.Queries;
using BankApp.App.Dispositions.Queries;
using BankApp.Data;
using BankApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.App.ViewModels
{
    public class AccountViewModel
    {
        private readonly BankAppDataContext context;
        Customer_Queries customer_queries;
        Account_Queries account_queries;
        public Customer Customer { get; set; }
        public Account Account { get; set; }

        public AccountViewModel(int accountId, int customerId)
        {
            this.context = new BankAppDataContext();
            this.customer_queries = new Customer_Queries(context);
            this.account_queries = new Account_Queries(context);

            Account = account_queries.GetAccount(accountId);
            Customer = customer_queries.GetCustomer(customerId);
        }
    }
}
