using BankApp.Data;
using BankApp.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankApp.App.Users.Queries
{
    public class UserQueriesHandler
    {
        private readonly BankAppDataContext context;

        public UserQueriesHandler(BankAppDataContext context)
        {
            this.context = context;
        }

        public List<User> GetUsers()
        {
            return context.Users.AsNoTracking().ToList();
        }

        public User GetUser(string id)
        {
            return context.Users.AsNoTracking().SingleOrDefault(u => u.Id == id);
        }
    }
}
