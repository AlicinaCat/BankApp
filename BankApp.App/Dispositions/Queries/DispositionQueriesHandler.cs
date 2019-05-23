using BankApp.Data;
using BankApp.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankApp.App.Dispositions.Queries
{
    public class DispositionQueriesHandler
    {
        private readonly BankAppDataContext context;

        public DispositionQueriesHandler(BankAppDataContext context)
        {
            this.context = context;
        }

        public List<Disposition> GetDispositions()
        {
            return context.Dispositions.ToList();
        }

        public List<Disposition> GetConnectedDispositions(int customerId)
        {
            return context.Dispositions.Include(d => d.Account).Where(d => d.Customer.CustomerId == customerId).ToList();
        }
    }
}
