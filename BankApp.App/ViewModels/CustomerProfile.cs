using BankApp.App.Accounts.Queries;
using BankApp.App.Customers.Queries;
using BankApp.App.Dispositions.Queries;
using BankApp.Data;
using BankApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.App.ViewModels
{
    public class CustomerProfile
    {
        private readonly BankAppDataContext context;
        DispositionQueriesHandler disposition_queries;
        CustomerQueriesHandler customer_queries;
        public Customer Customer { get; set; }
        public List<Disposition> ConnectedDispositions { get; set; }

        public CustomerProfile(int customerId)
        {
            this.context = new BankAppDataContext();
            this.customer_queries = new CustomerQueriesHandler(context);
            this.disposition_queries = new DispositionQueriesHandler(context);

            Customer = customer_queries.GetCustomer(customerId);
            this.ConnectedDispositions = disposition_queries.GetConnectedDispositions(Customer.CustomerId);
        }
    }
}
