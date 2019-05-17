using BankApp.App.Accounts.Queries;
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
        Disposition_Queries disposition_queries;
        Customer_Queries customer_queries;
        public Customer Customer { get; set; }
        public List<Disposition> ConnectedDispositions { get; set; }

        public CustomerProfile(int customerId)
        {
            this.context = new BankAppDataContext();
            this.customer_queries = new Customer_Queries(context);
            this.disposition_queries = new Disposition_Queries(context);

            Customer = customer_queries.GetCustomer(customerId);
            this.ConnectedDispositions = disposition_queries.GetConnectedDispositions(Customer.CustomerId);
        }
    }
}
