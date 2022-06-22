using Loan_API;
using Loan_API.Data;
using Loan_API.Domain;
using Loan_API.Models;

namespace Loan_API.Services
{
    public interface IUserService
    {
        User Register(RegistrationModel user);
        User Login(string username, string password);
        User GetOwnData();
    }

    public class UserService : IUserService
    {
        private UserContext _context;
        public UserService(UserContext context)
        {
            _context = context;
        }

        public User Register(RegistrationModel regData)
        {

            var user = new User();
            user.FirstName = regData.FirstName;
            user.LastName = regData.LastName;
            user.UserName = regData.UserName;
            user.Password = HashService.HashPassword(regData.Password);
            user.Age = regData.Age;
            user.Salary = regData.Salary;
            _context.Users.Add(user);
            return user;
        }

        public User Login(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public User GetOwnData()
        {
            throw new System.NotImplementedException();
        }
    }
}
