using BankApp.Data;
using BankApp.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankApp.App.Transactions.Queries
{
    public class TransactionQueryHandler
    {
        private readonly BankAppDataContext context;

        public TransactionQueryHandler()
        {
            this.context = new BankAppDataContext();
        }

        public TransactionQueryHandler(BankAppDataContext context)
        {
            this.context = context;
        }

        public List<Transaction> GetTransactionsByAccount(int id, int page)
        {
            const int pageSize = 20;

            var totalNumber = context.Transactions.AsNoTracking().ToList().Count;

            while (page * pageSize < totalNumber)
            {
                return context.Transactions.AsNoTracking().Where(t => t.AccountId == id).OrderByDescending(t => t.Date).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }

            return null;
        }
    }
}
