using BankApp.App.Customers.Commands;
using BankApp.App.Customers.Queries;
using BankApp.Data;
using BankApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.App.ViewModels
{
    public class EditCustomerViewModel
    {
        public Customer Customer { get; set; }
        public DateTime Birthday { get; set; }
        private readonly BankAppDataContext context;
        CustomerCommandHandler customerCommandHandler;
        CustomerQueriesHandler customerQueriesHandler;

        public EditCustomerViewModel()
        {
            this.context = new BankAppDataContext();
            this.customerCommandHandler = new CustomerCommandHandler(context);
            this.customerQueriesHandler = new CustomerQueriesHandler(context);
        }

        public EditCustomerViewModel(int id)
        {
            this.context = new BankAppDataContext();
            this.customerCommandHandler = new CustomerCommandHandler(context);
            this.customerQueriesHandler = new CustomerQueriesHandler(context);

            Customer = customerQueriesHandler.GetCustomer(id);
            Birthday = (DateTime)Customer.Birthday;
        }

        public void SaveChanges()
        {
            context.Update(Customer);
            context.SaveChanges();
        }
    }
}
