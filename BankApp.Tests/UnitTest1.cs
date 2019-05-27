using NUnit.Framework;
using NSubstitute;
using BankApp.App;
using BankApp.Domain;
using BankApp.Data;
using BankApp.App.Accounts.Commands;
using BankApp.App.Accounts.Queries;
using Microsoft.EntityFrameworkCore;
using BankApp.App.Customers.Commands;
using System.Linq;
using BankApp.App.Customers.Queries;
using BankApp.App.Dispositions.Queries;
using System;
using BankApp.Infrastructure.Services;

namespace Tests
{
    public class Tests
    {
        DbContextOptions<BankAppDataContext> options;
        BankAppDataContext context;
        AccountQueriesHandler accountQueriesHandler;
        AccountCommandHandler accountCommandHandler;
        CustomerCommandHandler customerCommandHandler;
        CustomerQueriesHandler customerQueriesHandler;
        DispositionQueriesHandler dispositionQueriesHandler;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<BankAppDataContext>()
                    .UseInMemoryDatabase(databaseName: "TestingDb")
                    .Options;

            context = new BankAppDataContext(options);
            accountQueriesHandler = new AccountQueriesHandler(context);
            accountCommandHandler = new AccountCommandHandler(context);
            customerCommandHandler = new CustomerCommandHandler(context);
            customerQueriesHandler = new CustomerQueriesHandler(context);
            dispositionQueriesHandler = new DispositionQueriesHandler(context);
        }

        [Test]
        public void AccountBalanceIncreased_WhenUserDeposit()
        {
            int accountId = 20;

            options = new DbContextOptionsBuilder<BankAppDataContext>()
                        .UseInMemoryDatabase(databaseName: "TestingDb")
                        .Options;
            using (var context = new BankAppDataContext(options))
            {
                accountQueriesHandler = new AccountQueriesHandler(context);
                accountCommandHandler = new AccountCommandHandler(context);

                context.Accounts.Add(new Account { AccountId = accountId, Balance = 2000 });
                context.SaveChanges();
            }

            using (var context = new BankAppDataContext(options))
            {
                accountQueriesHandler = new AccountQueriesHandler(context);
                accountCommandHandler = new AccountCommandHandler(context);

                decimal balanceBefore = accountQueriesHandler.GetAccount(accountId).Balance;
                decimal depositAmount = 1000;

                accountCommandHandler.Deposit(accountId, depositAmount);

                decimal balanceAfter = accountQueriesHandler.GetAccount(accountId).Balance;

                Assert.Greater(balanceAfter, balanceBefore);
            }
        }

        [Test]
        public void AccountBalanceDecreased_WhenUserWithdraws()
        {
            int accountId = 30;

            options = new DbContextOptionsBuilder<BankAppDataContext>()
            .UseInMemoryDatabase(databaseName: "TestingDb")
            .Options;
            using (var context = new BankAppDataContext(options))
            {
                accountQueriesHandler = new AccountQueriesHandler(context);
                accountCommandHandler = new AccountCommandHandler(context);

                context.Accounts.Add(new Account { AccountId = accountId, Balance = 2000 });
                context.SaveChanges();
            }

            using (var context = new BankAppDataContext(options))
            {
                accountQueriesHandler = new AccountQueriesHandler(context);
                accountCommandHandler = new AccountCommandHandler(context);

                decimal balanceBefore = accountQueriesHandler.GetAccount(accountId).Balance;
                decimal withdrawalAmount = 1000;

                accountCommandHandler.Withdraw(accountId, withdrawalAmount);

                decimal balanceAfter = accountQueriesHandler.GetAccount(accountId).Balance;

                Assert.Less(balanceAfter, balanceBefore);
            }
        }

        [Test]
        public void MoneyIsExchangedBetweenAccounts_WhenUserTransfers()
        {
            int accountFromId = 20;
            int accountToId = 30;

            decimal balanceFromBefore = accountQueriesHandler.GetAccount(accountFromId).Balance;
            decimal balanceToBefore = accountQueriesHandler.GetAccount(accountToId).Balance;
            decimal transferAmount = 100;

            accountCommandHandler.Transfer(accountFromId, accountToId, transferAmount);

            decimal balanceFromAfter = accountQueriesHandler.GetAccount(accountFromId).Balance;
            decimal balanceToAfter = accountQueriesHandler.GetAccount(accountToId).Balance;

            Assert.Less(balanceFromAfter, balanceFromBefore);
            Assert.Greater(balanceToAfter, balanceToBefore);
        }

        [Test]
        public void TransactionIsCreated_WhenUserDeposits()
        {
            int allTransactionsBefore = context.Transactions.CountAsync().Result;
            int accountId = 20;
            decimal depositAmount = 100;

            accountCommandHandler.Deposit(accountId, depositAmount);

            int allTransactionsAfter = context.Transactions.CountAsync().Result;

            Assert.AreEqual(allTransactionsAfter, allTransactionsBefore + 1);
        }

        [Test]
        public void TransactionIsCreated_WhenUserWithdraws()
        {
            int allTransactionsBefore = context.Transactions.CountAsync().Result;
            int accountId = 20;
            decimal withdrawalAmount = 100;

            accountCommandHandler.Withdraw(accountId, withdrawalAmount);

            int allTransactionsAfter = context.Transactions.CountAsync().Result;

            Assert.AreEqual(allTransactionsAfter, allTransactionsBefore + 1);
        }

        [Test]
        public void TransactionsAreCreated_WhenUserTransfers()
        {
            int allTransactionsBefore = context.Transactions.CountAsync().Result;

            int accountFromId = 20;
            int accountToId = 30;
            decimal transferAmount = 100;

            accountCommandHandler.Transfer(accountFromId, accountToId, transferAmount);

            int allTransactionsAfter = context.Transactions.CountAsync().Result;

            Assert.AreEqual(allTransactionsAfter, allTransactionsBefore + 2);
        }

        [Test]
        public void NewCustomerIsCreated_WhenUserCreatesCustomer()
        {
            int allCustomersBefore = context.Customers.CountAsync().Result;

            customerCommandHandler.CreateNewCustomer("female", "Smilla", "Meow", "Meow street 4", "Meow city", "12345", "Meowland", "MW", new System.DateTime(2013, 05, 02), "", "", "");

            int allCustomerAfter = context.Customers.CountAsync().Result;

            Assert.AreEqual(allCustomerAfter, allCustomersBefore + 1);
        }

        [Test]
        public void NewConnectedAccountIsCreated_WhenNewCustomerIsCreated()
        {
            customerCommandHandler.CreateNewCustomer("male", "Alfie", "Meow", "Meow street 4", "Meow city", "12345", "Meowland", "MW", new System.DateTime(2013, 05, 02), "", "", "");
            Customer customer = customerQueriesHandler.GetCustomersList().SingleOrDefault(c => c.Givenname == "Alfie" && c.Surname == "Meow");

            Disposition disposition = dispositionQueriesHandler.GetConnectedDispositions(customer.CustomerId).FirstOrDefault();

            Assert.IsNotNull(disposition);
        }

        [Test]
        public void WithdrawalFails_WhenBalanceIsTooSmall()
        {
            int accountId = 209;

            context.Accounts.Add(new Account { AccountId = accountId, Balance = 2000 });
            context.SaveChanges();

            decimal balanceBefore = accountQueriesHandler.GetAccount(accountId).Balance;
            decimal withdrawalAmount = 3000;

            accountCommandHandler.Withdraw(accountId, withdrawalAmount);

            decimal balanceAfter = accountQueriesHandler.GetAccount(accountId).Balance;

            Assert.AreEqual(balanceAfter, balanceBefore);
        }

        [Test]
        public void TransferFails_WhenBalanceIsTooSmall()
        {
            int accountFromId = 210;

            context.Accounts.Add(new Account { AccountId = accountFromId, Balance = 2000 });
            context.SaveChanges();

            int accountToId = 211;

            context.Accounts.Add(new Account { AccountId = accountToId, Balance = 1000 });
            context.SaveChanges();

            decimal accountFromBalanceBefore = accountQueriesHandler.GetAccount(accountFromId).Balance;
            decimal accountToBalanceBefore = accountQueriesHandler.GetAccount(accountToId).Balance;
            decimal transferAmount = 3000;

            accountCommandHandler.Transfer(accountFromId, accountToId, transferAmount);

            decimal accountFromBalanceAfter = accountQueriesHandler.GetAccount(accountFromId).Balance;
            decimal accountToBalanceAfter = accountQueriesHandler.GetAccount(accountToId).Balance;

            Assert.AreEqual(accountFromBalanceAfter, accountFromBalanceBefore);
            Assert.AreEqual(accountToBalanceAfter, accountToBalanceBefore);
        }

        [Test]
        public void DepositFails_WhenAmountIsNegative()
        {
            int accountId = 300;

            context.Accounts.Add(new Account { AccountId = accountId, Balance = 2000 });
            context.SaveChanges();

            decimal balanceBefore = accountQueriesHandler.GetAccount(accountId).Balance;
            decimal depositAmount = -30;

            accountCommandHandler.Deposit(accountId, depositAmount);

            decimal balanceAfter = accountQueriesHandler.GetAccount(accountId).Balance;

            Assert.AreEqual(balanceAfter, balanceBefore);
        }

        [Test]
        public void WithdrawalFails_WhenAmountIsNegative()
        {
            int accountId = 301;

            context.Accounts.Add(new Account { AccountId = accountId, Balance = 2000 });
            context.SaveChanges();

            decimal balanceBefore = accountQueriesHandler.GetAccount(accountId).Balance;
            decimal withdrawalAmount = -30;

            accountCommandHandler.Withdraw(accountId, withdrawalAmount);

            decimal balanceAfter = accountQueriesHandler.GetAccount(accountId).Balance;

            Assert.AreEqual(balanceAfter, balanceBefore);
        }

        [Test]
        public void TransferFails_WhenAmountIsNegative()
        {
            int accountFromId = 310;

            context.Accounts.Add(new Account { AccountId = accountFromId, Balance = 2000 });
            context.SaveChanges();

            int accountToId = 311;

            context.Accounts.Add(new Account { AccountId = accountToId, Balance = 1000 });
            context.SaveChanges();

            decimal accountFromBalanceBefore = accountQueriesHandler.GetAccount(accountFromId).Balance;
            decimal accountToBalanceBefore = accountQueriesHandler.GetAccount(accountToId).Balance;
            decimal transferAmount = -30;

            accountCommandHandler.Transfer(accountFromId, accountToId, transferAmount);

            decimal accountFromBalanceAfter = accountQueriesHandler.GetAccount(accountFromId).Balance;
            decimal accountToBalanceAfter = accountQueriesHandler.GetAccount(accountToId).Balance;

            Assert.AreEqual(accountFromBalanceAfter, accountFromBalanceBefore);
            Assert.AreEqual(accountToBalanceAfter, accountToBalanceBefore);
        }

        [Test]
        public void InterestIsCorrectlyApplied_WhenUserActivatesInterest()
        {
            int accountId = 77;

            options = new DbContextOptionsBuilder<BankAppDataContext>()
            .UseInMemoryDatabase(databaseName: "TestingDb")
            .Options;
            using (var context = new BankAppDataContext(options))
            {
                accountQueriesHandler = new AccountQueriesHandler(context);
                accountCommandHandler = new AccountCommandHandler(context);

                context.Accounts.Add(new Account { AccountId = accountId, Balance = 2000 });
                context.SaveChanges();
            }

            using (var context = new BankAppDataContext(options))
            {
                accountQueriesHandler = new AccountQueriesHandler(context);
                accountCommandHandler = new AccountCommandHandler(context);

                decimal balanceBefore = accountQueriesHandler.GetAccount(accountId).Balance;

                double rate = 0.02;
                DateTime latestInterestDate = new DateTime(2018, 02, 02);

                CustomDateTimeNowProvider provider = new CustomDateTimeNowProvider();
                provider.GivenDate = new DateTime(2019, 12, 02);
                var currentDate = provider.Now;
                
                accountCommandHandler.ApplyInterest(accountId, rate, latestInterestDate, currentDate);

                decimal balanceAfter = accountQueriesHandler.GetAccount(accountId).Balance;
                double days = (currentDate - latestInterestDate).TotalDays;

                Assert.AreEqual(balanceAfter, balanceBefore + (decimal)((double)balanceBefore * rate / 365 * days));
            }
        }

        [Test]
        public void TransactionIsCreated_WhenUserAppliesInterest()
        {
            int allTransactionsBefore = context.Transactions.CountAsync().Result;
            int accountId = 77;

            double rate = 0.02;
            DateTime latestInterestDate = new DateTime(2018, 02, 02);

            CustomDateTimeNowProvider provider = new CustomDateTimeNowProvider();
            provider.GivenDate = new DateTime(2019, 12, 02);
            var currentDate = provider.Now;
            accountCommandHandler.ApplyInterest(accountId, rate, latestInterestDate, currentDate);

            int allTransactionsAfter = context.Transactions.CountAsync().Result;

            Assert.AreEqual(allTransactionsAfter, allTransactionsBefore + 1);
        }
    }
}