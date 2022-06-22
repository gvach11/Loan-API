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
    public interface ILoanService
    {
        public Loan AddLoan(AddLoanModel loanModel, int userId);
    }

    public class LoanService : ILoanService
    {
        private UserContext _context;
        private readonly AppSettings _appSettings;
        public LoanService(UserContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }
        public Loan AddLoan(AddLoanModel loanModel, int userId)
        {
            var newLoan = new Loan();
            newLoan.UserId = userId;
            newLoan.Type = loanModel.LoanType;
            newLoan.Currency = loanModel.Currency;
            newLoan.Amount = loanModel.Amount;
            newLoan.Period = loanModel.LoanPeriod;
            _context.Loans.Add(newLoan);
            _context.SaveChanges();
            return newLoan;

        }

    }
}
