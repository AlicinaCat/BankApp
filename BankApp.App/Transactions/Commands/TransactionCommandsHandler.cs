using System;
using BankApp.Data;
using BankApp.Domain;

namespace BankApp.App.Transactions.Commands
{
    public class TransactionCommandsHandler
    {
        private readonly BankAppDataContext context;

        public TransactionCommandsHandler(BankAppDataContext context)
        {
            this.context = context;
        }

        public void CreateTransaction(int accountId, decimal amount, decimal balance, string operation, string type)
        {
            Transaction transaction = new Transaction()
            {
                AccountId = accountId,
                Date = DateTime.Now.Date,
                Operation = operation,
                Amount = amount,
                Balance = balance,
                Type = type
            };

            context.Add(transaction);
            context.SaveChanges();
        }
    }
}
