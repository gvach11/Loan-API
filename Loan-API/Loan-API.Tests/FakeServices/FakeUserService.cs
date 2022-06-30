using Loan_API.Domain;
using Loan_API.Helpers;
using Loan_API.Models;
using Loan_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_API.Tests.FakeServices
{
    public class FakeUserService : IUserService
    {
        public User Authenticate(string username, string password)
        {
            User tempUser = new User()
            {
                Id = 1,
                FirstName = "FirstName",
                LastName = "LastName",
                Age = 30,
                Salary = 999,
                UserName = "Accountant",
                Password = HashService.HashPassword("12345"),
                Role = "Accountant",
                IsBlocked = false,
                Loans = new List<Loan> { new Loan { Id = 1},
            new Loan { Id = 2}}
            };
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;


            if (HashService.HashPassword(password) != tempUser.Password)
                return null;

            return tempUser;
        }

        public string GenerateToken(User user)
        {
            throw new NotImplementedException();
        }

        public User GetOwnData()
        {
            throw new NotImplementedException();
        }

        public User Login(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Register(RegistrationModel user)
        {
            throw new NotImplementedException();
        }
    }
}
