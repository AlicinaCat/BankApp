using System;
using System.Collections.Generic;
using System.Linq;
using BankApp.Data;
using BankApp.Domain;

namespace BankApp.App.Accounts.Queries
{
    public class GetAllCards
    {
        private readonly BankAppDataContext context;
       
        public GetAllCards(BankAppDataContext context)
        {
            this.context = context;
        }

        public List<Card> Get()
        {
            return context.Cards.ToList();
        }
    }
}
