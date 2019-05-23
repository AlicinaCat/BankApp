using BankApp.App.Dispositions.Commands;
using BankApp.Data;
using BankApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.App.Customers.Commands
{
    public class CustomerCommandHandler
    {
        private readonly BankAppDataContext context;
        private DispositionCommandHandler dispositionCommandHandler;

        public CustomerCommandHandler(BankAppDataContext context)
        {
            this.context = context;
            dispositionCommandHandler = new DispositionCommandHandler(context);
        }

        public void CreateNewCustomer(string gender, string givenName, string surname, string streetAddress, string city, string Zipcode, string country, string countryCode, DateTime birthDate, string nationalId = "",
                                        string telephoneCountryCode = "", string telephoneNumber = "", string emailAddress = "")
        {
            Customer customer = new Customer()
            {
                Gender = gender,
                Givenname = givenName,
                Surname = surname,
                Streetaddress = streetAddress,
                City = city,
                Zipcode = Zipcode,
                Country = country,
                CountryCode = countryCode,
                Birthday = birthDate.Date,
                NationalId = nationalId,
                Telephonecountrycode = telephoneCountryCode, 
                Telephonenumber = telephoneNumber,
                Emailaddress = emailAddress
            };

            context.Add(customer);
            context.SaveChanges();

            dispositionCommandHandler.CreateNewDisposition(customer.CustomerId);
        }
    }
}
