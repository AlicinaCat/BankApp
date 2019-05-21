using System;
using System.Collections.Generic;
using System.Linq;
using BankApp.Data;
using BankApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace BankApp.App.Customers.Queries
{
    public class Customer_Queries
    {
        private readonly BankAppDataContext context;

        public Customer_Queries(BankAppDataContext context)
        {
            this.context = context;
        }

        public List<Customer> GetCustomers()
        {
            return context.Customers.ToList();
        }

        public Customer GetCustomer(int id)
        {
            return context.Customers.SingleOrDefault(c => c.CustomerId == id);
        }

        public IQueryable<Customer> SearchCustomers(string userInput)
        {

                userInput = userInput.ToLower();

                if (int.TryParse(userInput, out int result))
                {
                    return context.Customers.Where(c => c.CustomerId == result);
                }
                else
                {
                    return context.Customers.Where(c => c.Givenname.ToLower().Contains(userInput)
                                                || c.Surname.ToLower().Contains(userInput)
                                                || c.City.ToLower().Contains(userInput));
                                              
                }
            
        }
    }
}
