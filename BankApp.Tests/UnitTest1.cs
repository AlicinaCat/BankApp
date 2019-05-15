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
        Account_Queries queries;
        Account_Actions actions;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<BankAppDataContext>()
                    .UseInMemoryDatabase(databaseName: "CheckDeposits")
                    .Options;

            context = new BankAppDataContext(options);
            queries = new Account_Queries(context);
            actions = new Account_Actions(context, queries);
        }

        [Test]
        public void AccountBalanceIncreased_WhenUserDeposit()
        {
            int accountId = 1;

            context.Accounts.Add(new Account { AccountId = accountId, Balance = 2000 });
            context.SaveChanges();

            decimal balanceBefore = queries.GetAccount(accountId).Balance;
            decimal depositAmount = 1000;

            actions.Deposit(accountId, depositAmount);

            decimal balanceAfter = queries.GetAccount(accountId).Balance;

            Assert.Greater(balanceAfter, balanceBefore);
        }

        [Test]
        public void AccountBalanceDecreased_WhenUserWithdraws()
        {
            int accountId = 2;

            context.Accounts.Add(new Account { AccountId = accountId, Balance = 2000 });
            context.SaveChanges();

            decimal balanceBefore = queries.GetAccount(accountId).Balance;
            decimal withdrawalAmount = 1000;

            actions.Withdraw(accountId, withdrawalAmount);

            decimal balanceAfter = queries.GetAccount(accountId).Balance;

            Assert.Less(balanceAfter, balanceBefore);
        }

        [Test]
        public void TransactionIsCreated_WhenUserDeposits()
        {
            int allTransactionsBefore = context.Transactions.CountAsync().Result;
            int accountId = 1;
            decimal depositAmount = 100;

            actions.Deposit(accountId, depositAmount);

            int allTransactionsAfter = context.Transactions.CountAsync().Result;

            Assert.AreEqual(allTransactionsAfter, allTransactionsBefore + 1);
        }
    }
}