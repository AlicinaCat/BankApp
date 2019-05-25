using BankApp.App.Customers.Commands;
using BankApp.App.Customers.Queries;
using BankApp.App.Users.Queries;
using BankApp.Data;
using BankApp.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankApp.App.ViewModels
{
    public class ManageUsersViewModel
    {
        public List<UserWithRoles> UsersWithRoles { get; set; }
    } 
}
