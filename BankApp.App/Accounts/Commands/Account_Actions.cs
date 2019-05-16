using System;
using BankApp.App.Accounts.Queries;
using BankApp.App.Transactions.Commands;
using BankApp.Data;
using BankApp.Domain;

namespace BankApp.App.Accounts.Commands
{
    public class Account_Actions
    {
        private readonly BankAppDataContext context;
        private Account_Queries account_queries;
        private Transaction_Actions transaction_actions;

        public Account_Actions(BankAppDataContext context, Account_Queries queries)
        {
            this.context = context;
            this.account_queries = queries;
            this.transaction_actions= new Transaction_Actions(context);
        }

        public void Deposit(int accountId, decimal amount)
        {
            Account account = account_queries.GetAccount(accountId);

            account.Balance += amount;
            context.Update(account);
            context.SaveChanges();

            transaction_actions.CreateTransaction(accountId, amount, account.Balance, "Deposit", "Credit");
        }

        public void Withdraw(int accountId, decimal amount)
        {
            Account account = account_queries.GetAccount(accountId);

            account.Balance -= amount;
            context.Update(account);
            context.SaveChanges();

            transaction_actions.CreateTransaction(accountId, amount, account.Balance, "Withdrawal", "Debit");

            // TODO - send negative amount in withdrawal
        }

        public void Transfer(int accountFromId, int accountToId, decimal amount)
        {
            Withdraw(accountFromId, amount);
            Deposit(accountToId, amount);
        }
    }
}
