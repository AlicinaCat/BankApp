﻿using System;
using System.Collections.Generic;
using System.Linq;
using BankApp.Data;
using BankApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace BankApp.App.Customers.Queries
{
    public class CustomerQueriesHandler
    {
        private readonly BankAppDataContext context;

        public CustomerQueriesHandler()
        {
            this.context = new BankAppDataContext();
        }

        public CustomerQueriesHandler(BankAppDataContext context)
        {
            this.context = context;
        }

        public List<Customer> GetCustomersList()
        {
            return context.Customers.ToList();
        }

        public Customer GetCustomer(int id)
        {
            return context.Customers.SingleOrDefault(c => c.CustomerId == id);
        }

        public IQueryable<Customer> SearchCustomers(string userInput)
        {

                userInput = userInput.ToLower().Trim().Replace(" ", String.Empty);

                if (int.TryParse(userInput, out int result))
                {
                    return context.Customers.Where(c => c.CustomerId == result);
                }
                else
                {
                return context.Customers.Where(c => (c.Givenname + c.Surname + c.City).ToLower().Contains(userInput) || (c.Givenname + c.City).ToLower().Contains(userInput));
                                              
                }
            
        }
    }
}
