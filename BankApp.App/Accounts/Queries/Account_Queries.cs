using System;
using System.Collections.Generic;
using System.Linq;
using BankApp.Data;
using BankApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace BankApp.App.Accounts.Queries
{
    public class Account_Queries
    {
        private readonly BankAppDataContext context;
       
        public Account_Queries(BankAppDataContext context)
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
