using System;
using BankApp.App.Accounts.Queries;
using BankApp.App.Transactions.Commands;
using BankApp.Data;
using BankApp.Domain;

namespace BankApp.App.Accounts.Commands
{
    public class AccountCommandHandler
    {
        private readonly BankAppDataContext context;
        private AccountQueriesHandler accountQueriesHandler;
        private TransactionCommandsHandler transactionCommandsHandler;

        public AccountCommandHandler()
        {
            this.context = new BankAppDataContext();
            this.accountQueriesHandler = new AccountQueriesHandler(context);
            this.transactionCommandsHandler = new TransactionCommandsHandler(context);
        }

        public AccountCommandHandler(BankAppDataContext context)
        {
            this.context = context;
            this.accountQueriesHandler = new AccountQueriesHandler(context);
            this.transactionCommandsHandler= new TransactionCommandsHandler(context);
        }

        public void Deposit(int accountId, decimal amount)
        {
            Account account = accountQueriesHandler.GetAccount(accountId);

            account.Balance += amount;
            context.Update(account);
            context.SaveChanges();

            transactionCommandsHandler.CreateTransaction(accountId, amount, account.Balance, "Deposit", "Credit");
        }

        public void Withdraw(int accountId, decimal amount)
        {
            Account account = accountQueriesHandler.GetAccount(accountId);

            account.Balance -= amount;
            context.Update(account);
            context.SaveChanges();

            transactionCommandsHandler.CreateTransaction(accountId, amount, account.Balance, "Withdrawal", "Debit");

            // TODO - send negative amount in withdrawal
        }

        public void Transfer(int accountFromId, int accountToId, decimal amount)
        {
            Withdraw(accountFromId, amount);
            Deposit(accountToId, amount);
        }

        public int CreateNewAccount()
        {
            Account account = new Account()
            {
                Frequency = "Monthly",
                Created = DateTime.Now,
                Balance = 0
            };

            context.Add(account);
            context.SaveChanges();

            return account.AccountId;
        }
    }
}
