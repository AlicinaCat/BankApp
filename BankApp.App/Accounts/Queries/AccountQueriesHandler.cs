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
            return context.Accounts.AsNoTracking().ToList();
        }

        public Account GetAccount(int accountId)
        {
            return context.Accounts.AsNoTracking().SingleOrDefault(a => a.AccountId == accountId);
        }

        public Account GetAccountWithTransactions(int accountId)
        {
            return context.Accounts.AsNoTracking().Include(a => a.Transactions).SingleOrDefault(a => a.AccountId == accountId);
        }

        public decimal CalculateTotalBalance()
        {
            return context.Accounts.Sum(a => a.Balance);
        }
    }
}
