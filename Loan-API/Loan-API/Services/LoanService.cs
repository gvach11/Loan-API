﻿using Loan_API;
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
        public IQueryable<Loan> GetOwnLoans(int userId);
        public Loan UpdateOwnLoan(UpdateLoanModel model);
        public Loan DeleteOwnLoan(int loanId);
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

        public IQueryable<Loan> GetOwnLoans(int userId)
        {
            return _context.Loans.Where(loan => loan.UserId == userId);
        }

        public Loan UpdateOwnLoan(UpdateLoanModel model)
        {
            var tempLoan = new Loan() { Id = model.LoanId};
            if (model.LoanType != null) tempLoan.Type = model.LoanType;
            else tempLoan.Type = _context.Loans.Where(loan => loan.Id == model.LoanId).FirstOrDefault().Type;
            if (model.Currency != null) tempLoan.Currency = model.Currency;
            else tempLoan.Currency = _context.Loans.Where(loan => loan.Id == model.LoanId).FirstOrDefault().Currency;
            if (model.Amount != 0) tempLoan.Amount = model.Amount;
            else tempLoan.Amount = _context.Loans.Where(loan => loan.Id == model.LoanId).FirstOrDefault().Amount;
            if (model.LoanPeriod != 0) tempLoan.Period = model.LoanPeriod;
            else tempLoan.Period = _context.Loans.Where(loan => loan.Id == model.LoanId).FirstOrDefault().Period;
            return tempLoan;

        }

        public Loan DeleteOwnLoan(int loanId)
        {
            var loanToDelete = _context.Loans.Find(loanId);
            _context.Loans.Remove(loanToDelete);
            _context.SaveChanges();
            return loanToDelete;
        }
    }
}
