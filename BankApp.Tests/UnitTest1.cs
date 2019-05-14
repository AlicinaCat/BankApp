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
        //IUserDataStore userDataStore;

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void AccountBalanceIncreased_WhenUserDeposit()
        {
            var options = new DbContextOptionsBuilder<BankAppDataContext>()
                    .UseInMemoryDatabase(databaseName: "CheckDeposits")
                    .Options;
                    

            int accountId = 1;

            decimal balanceBefore = 0;

            using (var context = new BankAppDataContext(options))
            {
                GetQueries queries = new GetQueries(context);
                Actions actions = new Actions(context, queries);

                context.Accounts.Add(new Account { AccountId = accountId, Balance = 2000 });
                context.SaveChanges();

                balanceBefore = queries.GetAccount(accountId).Balance;
                decimal depositAmount = 1000;

                actions.Deposit(accountId, depositAmount);
            }

            using (var context = new BankAppDataContext(options))
            {
                GetQueries queries = new GetQueries(context);
                Actions actions = new Actions(context, queries);
                decimal balanceAfter = queries.GetAccount(accountId).Balance;


                Assert.Greater(balanceAfter, balanceBefore);
            }
            
        }
    }
}