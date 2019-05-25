using System;
using System.Collections.Generic;
using System.Linq;
using BankApp.Data;
using BankApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace BankApp.App.Accounts.Queries
{
    public class AccountQueriesHandler
    {
        private readonly BankAppDataContext context;

        public AccountQueriesHandler()
        {
            this.context = new BankAppDataContext();
        }

        public AccountQueriesHandler(BankAppDataContext context)
        {
            this.context = context;
        }

        public List<Account> GetAllAccounts()
        {
            return context.Accounts.ToList();
        }

        public Account GetAccount(int accountId)
        {
            return context.Accounts.Include(a => a.Transactions).SingleOrDefault(a => a.AccountId == accountId);
        }

        public decimal CalculateTotalBalance()
        {
            return context.Accounts.Sum(a => a.Balance);
        }
    }
}
