using NUnit.Framework;
using NSubstitute;
using BankApp.App;
using BankApp.Domain;
using BankApp.Data;
using BankApp.App.Accounts.Commands;
using BankApp.App.Accounts.Queries;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    public class Tests
    {
        DbContextOptions<BankAppDataContext> options;
        BankAppDataContext context;
        AccountQueriesHandler account_queries;
        AccountCommandHandler account_actions;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<BankAppDataContext>()
                    .UseInMemoryDatabase(databaseName: "TestingDb")
                    .Options;

            context = new BankAppDataContext(options);
            account_queries = new AccountQueriesHandler(context);
            account_actions = new AccountCommandHandler(context, account_queries);
        }

        [Test]
        public void AccountBalanceIncreased_WhenUserDeposit()
        {
            int accountId = 1;

            context.Accounts.Add(new Account { AccountId = accountId, Balance = 2000 });
            context.SaveChanges();

            decimal balanceBefore = account_queries.GetAccount(accountId).Balance;
            decimal depositAmount = 1000;

            account_actions.Deposit(accountId, depositAmount);

            decimal balanceAfter = account_queries.GetAccount(accountId).Balance;

            Assert.Greater(balanceAfter, balanceBefore);
        }

        [Test]
        public void AccountBalanceDecreased_WhenUserWithdraws()
        {
            int accountId = 2;

            context.Accounts.Add(new Account { AccountId = accountId, Balance = 2000 });
            context.SaveChanges();

            decimal balanceBefore = account_queries.GetAccount(accountId).Balance;
            decimal withdrawalAmount = 1000;

            account_actions.Withdraw(accountId, withdrawalAmount);

            decimal balanceAfter = account_queries.GetAccount(accountId).Balance;

            Assert.Less(balanceAfter, balanceBefore);
        }

        [Test]
        public void MoneyIsExchangedBetweenAccounts_WhenUserTransfers()
        {
            int accountFromId = 1;
            int accountToId = 2;

            decimal balanceFromBefore = account_queries.GetAccount(accountFromId).Balance;
            decimal balanceToBefore = account_queries.GetAccount(accountToId).Balance;
            decimal transferAmount = 100;

            account_actions.Transfer(accountFromId, accountToId, transferAmount);

            decimal balanceFromAfter = account_queries.GetAccount(accountFromId).Balance;
            decimal balanceToAfter = account_queries.GetAccount(accountToId).Balance;

            Assert.Less(balanceFromAfter, balanceFromBefore);
            Assert.Greater(balanceToAfter, balanceToBefore);
        }

        [Test]
        public void TransactionIsCreated_WhenUserDeposits()
        {
            int allTransactionsBefore = context.Transactions.CountAsync().Result;
            int accountId = 1;
            decimal depositAmount = 100;

            account_actions.Deposit(accountId, depositAmount);

            int allTransactionsAfter = context.Transactions.CountAsync().Result;

            Assert.AreEqual(allTransactionsAfter, allTransactionsBefore + 1);
        }

        [Test]
        public void TransactionIsCreated_WhenUserWithdraws()
        {
            int allTransactionsBefore = context.Transactions.CountAsync().Result;
            int accountId = 1;
            decimal withdrawalAmount = 100;

            account_actions.Withdraw(accountId, withdrawalAmount);

            int allTransactionsAfter = context.Transactions.CountAsync().Result;

            Assert.AreEqual(allTransactionsAfter, allTransactionsBefore + 1);
        }

        [Test]
        public void TransactionsAreCreated_WhenUserTransfers()
        {
            int allTransactionsBefore = context.Transactions.CountAsync().Result;

            int accountFromId = 1;
            int accountToId = 2;
            decimal transferAmount = 100;

            account_actions.Transfer(accountFromId, accountToId, transferAmount);

            int allTransactionsAfter = context.Transactions.CountAsync().Result;

            Assert.AreEqual(allTransactionsAfter, allTransactionsBefore + 2);
        }
    }
}