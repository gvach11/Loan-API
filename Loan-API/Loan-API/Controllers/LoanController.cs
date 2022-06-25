using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Loan_API.Domain;
using Loan_API.Data;
using Microsoft.AspNetCore.Authorization;
using Loan_API.Services;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Loan_API.Models;
using Loan_API.Helpers;

namespace Loan_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class LoanController : ControllerBase
    {
        private readonly UserContext _context;
        private ILoanService _loanService;
        public LoanController(UserContext context, ILoanService loanService)
        {
            _context = context;
            _loanService = loanService;
        }

        internal int GetUid()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userIdString = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            var userId = Convert.ToInt32(userIdString);
            return userId;
        }

        [Authorize(Roles = Roles.User)]
        [HttpPost("addloan")]
        public IActionResult AddLoan(AddLoanModel addLoanModel)
        {
            LoanValidator validator = new LoanValidator(_context);
            var result = validator.Validate(addLoanModel);
            if (!result.IsValid)
            {
                return BadRequest(ValidationErrorParse.GetErrors(result));
            }
            var userId = GetUid();
            if (_context.Users.Find(userId).IsBlocked == true) return Unauthorized("The user is blocked");
            _loanService.AddLoan(addLoanModel, userId);
            return Ok("Loan Created");
            
        }

        [Authorize(Roles = Roles.User)]
        [HttpGet("getownloans")]
        public IActionResult GetOwnLoans()
        {
            var userId = GetUid();
            return Ok(_loanService.GetOwnLoans(userId));
        }

        [Authorize(Roles = Roles.User)]
        [HttpPut("updateownloan")]
        public IActionResult UpdateOwnLoan(UpdateLoanModel model)
        {
            LoanValidator validator = new LoanValidator(_context);
            var userId = GetUid();
            var tempLoan = _loanService.UpdateOwnLoan(model);
            tempLoan.UserId = userId;
            if (tempLoan.UserId != _context.Loans.Find(model.LoanId).UserId) return Unauthorized("You are not allowed to modify this loan. Reason: Not your loan");
            if (tempLoan.Status != LoanStatus.Processing) return Unauthorized("You are not allowed to modify this loan. Reason: Loan already processed");
            var verifiableLoan = validator.ConvertToValidatable(tempLoan);
            var result = validator.Validate(verifiableLoan);
            if (!result.IsValid)
            {
                return BadRequest(ValidationErrorParse.GetErrors(result));
            }
            _context.Loans.Update(tempLoan);
            _context.SaveChanges();
            return Ok("Loan Updated");
        }

        [Authorize(Roles = Roles.User)]
        [HttpDelete("deleteownloan")]
        public async Task<IActionResult> DeleteOwnLoan(DeleteLoanModel model)
        {
            var userId = GetUid();
            IQueryable<Loan> ownLoans = _loanService.GetOwnLoans(userId);
            var loanToCheck = ownLoans.Where(loan => loan.Id == model.LoanId).FirstOrDefault();
            if (loanToCheck == null) return NotFound($"Loan not found");
            if (loanToCheck.Status != LoanStatus.Processing) return Unauthorized("You are not allowed to modify this loan. Reason: Loan already processed");
            _loanService.DeleteOwnLoan(model.LoanId);
            return Ok("Loan Deleted");
        }


    }
}
