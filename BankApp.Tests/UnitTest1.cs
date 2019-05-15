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
        GetQueries queries;
        Actions actions;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<BankAppDataContext>()
                    .UseInMemoryDatabase(databaseName: "CheckDeposits")
                    .Options;

            context = new BankAppDataContext(options);
            queries = new GetQueries(context);
            actions = new Actions(context, queries);

        }

        [Test]
        public void AccountBalanceIncreased_WhenUserDeposit()
        {
            int accountId = 1;

            decimal balanceBefore = 0;

            context.Accounts.Add(new Account { AccountId = accountId, Balance = 2000 });
            context.SaveChanges();

            balanceBefore = queries.GetAccount(accountId).Balance;
            decimal depositAmount = 1000;

            actions.Deposit(accountId, depositAmount);

            decimal balanceAfter = queries.GetAccount(accountId).Balance;


            Assert.Greater(balanceAfter, balanceBefore);


        }

        [Test]
        public void AccountBalanceDecreased_WhenUserWithdraws()
        {
            int accountId = 2;

            decimal balanceBefore = 0;

            using (var context = new BankAppDataContext(options))
            {
                GetQueries queries = new GetQueries(context);
                Actions actions = new Actions(context, queries);

                context.Accounts.Add(new Account { AccountId = accountId, Balance = 2000 });
                context.SaveChanges();

                balanceBefore = queries.GetAccount(accountId).Balance;
                decimal withdrawalAmount = 1000;

                //actions.Withdraw(accountId, withdrawalAmount);
            }
        }
    }
}