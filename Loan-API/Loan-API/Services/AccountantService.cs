using Loan_API;
using Loan_API.Data;
using Loan_API.Domain;
using Loan_API.Models;
using Loan_API.Helpers;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Http;

namespace Loan_API.Services
{
    public interface IAccountantService
    {
        public IQueryable<Loan> GetAnyLoan(string loanId);
        public Loan UpdateAnyLoan(UpdateLoanModel model);
        public Loan DeleteAnyLoan(int loanId);
        public User BlockUser(int userId);
        public User UnblockUser(int userId);
    }

    public class AccountService : IAccountantService
    {
        private UserContext _context;
        private readonly AppSettings _appSettings;
        public AccountService(UserContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }
        public User BlockUser(int userId)
        {
            var tempUser = _context.Users.Find(userId);
            tempUser.IsBlocked = true;
            _context.Users.Update(tempUser);
            _context.SaveChanges();
            return tempUser;
        }
        public User UnblockUser(int userId)
        {
            var tempUser = _context.Users.Find(userId);
            tempUser.IsBlocked = false;
            _context.Users.Update(tempUser);
            _context.SaveChanges();
            return tempUser;
        }

        public Loan DeleteAnyLoan(int loanId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Loan> GetAnyLoan(string loanId)
        {
            throw new NotImplementedException();
        }

        public Loan UpdateAnyLoan(UpdateLoanModel model)
        {
            throw new NotImplementedException();
        }
    }
}
