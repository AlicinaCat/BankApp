using System;
using BankApp.Domain;

namespace BankApp.Domain
{
    public class Bank
    {
        private IUserDataStore userDataStore;

        public Bank(IUserDataStore userDataStore)
        {
            this.userDataStore = userDataStore;
        }

        public Account GetAccount(string accountId)
        {
            return userDataStore.GetAccount(accountId);
        }

        public void Deposit(string accountId, decimal amount)
        {
            //GetAccount(accountId).Deposit(amount);
        }

        public void Withdraw(string accountId, decimal amount)
        {
            //GetAccount(accountId).Withdraw(amount);
        }

        public void Transfer(string accountFromId, string accountToId, decimal amount)
        {
            //GetAccount(accountFromId).Withdraw(amount);
            //GetAccount(accountToId).Deposit(amount);
        }
    }
}
