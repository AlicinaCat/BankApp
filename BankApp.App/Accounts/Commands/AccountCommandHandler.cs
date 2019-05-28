using System;
using BankApp.App.Accounts.Queries;
using BankApp.App.Transactions.Commands;
using BankApp.Data;
using BankApp.Domain;
using BankApp.Infrastructure.Services;

namespace BankApp.App.Accounts.Commands
{
    public class AccountCommandHandler
    {
        private readonly BankAppDataContext context;
        private AccountQueriesHandler accountQueriesHandler;
        private TransactionCommandsHandler transactionCommandsHandler;
        private readonly ISystemClock systemClock;

        public AccountCommandHandler()
        {
            this.context = new BankAppDataContext();
            this.accountQueriesHandler = new AccountQueriesHandler(context);
            this.transactionCommandsHandler = new TransactionCommandsHandler(context);
            this.systemClock = new SystemClock();
        }

        public AccountCommandHandler(BankAppDataContext context)
        {
            this.context = context;
            this.accountQueriesHandler = new AccountQueriesHandler(context);
            this.transactionCommandsHandler = new TransactionCommandsHandler(context);
            this.systemClock = new SystemClock();
        }

        public AccountCommandHandler(BankAppDataContext context, ISystemClock systemClock)
        {
            this.context = context;
            this.accountQueriesHandler = new AccountQueriesHandler(context);
            this.transactionCommandsHandler = new TransactionCommandsHandler(context);
            this.systemClock = systemClock;
        }

        public int Deposit(int accountId, decimal amount)
        {
            if (amount > 0)
            {
                Account account = accountQueriesHandler.GetAccount(accountId);

                if (account != null)
                {
                    account.Balance += amount;
                    context.Update(account);
                    context.SaveChanges();

                    transactionCommandsHandler.CreateTransaction(accountId, amount, account.Balance, "Deposit", "Credit");

                    return 1;
                }
            }

            return -1;
        }

        public int Withdraw(int accountId, decimal amount)
        {
            Account account = accountQueriesHandler.GetAccount(accountId);

            if (account != null)
            {

                if (amount <= account.Balance && amount > 0)
                {
                    account.Balance -= amount;
                    context.Update(account);
                    context.SaveChanges();

                    transactionCommandsHandler.CreateTransaction(accountId, -amount, account.Balance, "Withdrawal", "Debit");

                    return 1;
                }
            }

            return -1;
        }

        public int Transfer(int accountFromId, int accountToId, decimal amount)
        {
            Account accountFrom = accountQueriesHandler.GetAccount(accountFromId);
            Account accountTo = accountQueriesHandler.GetAccount(accountToId);

            if (accountTo != null && accountFrom != null)
            {
                int result = Withdraw(accountFromId, amount);

                if (result == 1)
                {
                    result = Deposit(accountToId, amount);

                    if (result == 1)
                    {
                        return 1;
                    }
                }
            }

            return -1;
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

        public int ApplyInterest(int accountId, double rate, DateTime latestInterestDate)
        {
            Account account = accountQueriesHandler.GetAccount(accountId);

            rate /= 100;
            DateTime currentDate = systemClock.GetCurrentTime();
            double days = (currentDate - latestInterestDate).TotalDays;
            decimal amount = (decimal)((double)account.Balance * rate / 365 * days);
            amount = Decimal.Round(amount, 2);
            account.Balance += amount;

            context.Update(account);
            context.SaveChanges();

            transactionCommandsHandler.CreateTransaction(accountId, amount, account.Balance, "Interest", "Credit");

            return 1;
        }
    }
}
