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
        [Authorize(Roles = Roles.User)]
        [HttpPost("addloan")]
        public IActionResult AddLoan(AddLoanModel addLoanModel)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userIdString = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            var userId = Convert.ToInt32(userIdString);
            if (_context.Users.Find(userId).IsBlocked == true) return Unauthorized("The user is blocked");
            _loanService.AddLoan(addLoanModel, userId);
            return Ok("Loan Created");
            
        }

    }
}
