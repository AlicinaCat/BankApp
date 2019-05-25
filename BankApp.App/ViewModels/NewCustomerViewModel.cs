using BankApp.App.Customers.Commands;
using BankApp.App.Customers.Queries;
using BankApp.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.App.ViewModels
{
    public class NewCustomerViewModel
    {
        public string Gender { get; set; }
        public string Givenname { get; set; }
        public string Surname { get; set; }
        public string Streetaddress { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public DateTime Birthday { get; set; }
        public string NationalId { get; set; }
        public string Telephonecountrycode { get; set; }
        public string Telephonenumber { get; set; }
        public string Emailaddress { get; set; }
        private readonly BankAppDataContext context;
        CustomerCommandHandler customerCommandHandler;
        CustomerQueriesHandler customerQueriesHandler;

        public NewCustomerViewModel()
        {
            this.context = new BankAppDataContext();
            this.customerCommandHandler = new CustomerCommandHandler(context);
            this.customerQueriesHandler = new CustomerQueriesHandler(context);
        }

        public int CreateNewCustomer()
        {
            return customerCommandHandler.CreateNewCustomer(Gender, Givenname, Surname, Streetaddress, City, Zipcode, Country, CountryCode, Birthday, Telephonecountrycode, Telephonenumber, Emailaddress);
        }
    }
}
