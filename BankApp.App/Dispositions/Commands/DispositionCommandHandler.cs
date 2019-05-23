using BankApp.App.Accounts.Commands;
using BankApp.Data;
using BankApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.App.Dispositions.Commands
{
    public class DispositionCommandHandler
    {
        private BankAppDataContext context;
        AccountCommandHandler accountCommandHandler;

        public DispositionCommandHandler(BankAppDataContext context)
        {
            this.context = context;
            accountCommandHandler = new AccountCommandHandler(context);
        }

        public void CreateNewDisposition(int customerId)
        {
            Disposition disposition = new Disposition()
            {
                CustomerId = customerId,
                AccountId = accountCommandHandler.CreateNewAccount(),
                Type = "OWNER"
            };

            context.Add(disposition);
            context.SaveChanges();
        }
    }
}
