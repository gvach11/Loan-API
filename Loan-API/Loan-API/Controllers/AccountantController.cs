using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using FluentValidation.Results;
using Loan_API.Domain;
using Loan_API.Data;
using Loan_API.Services;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Loan_API.Models;
using Loan_API.Helpers;
using System.Threading.Tasks;

namespace Loan_API.Controllers
{
    [Authorize(Roles = Roles.Accountant)]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountantController : ControllerBase
    {
        private readonly UserContext _context;
        private IAccountantService _accountantService;
        public AccountantController(UserContext context, IAccountantService accountantService)
        {
            _context = context;
            _accountantService = accountantService;
        }


        [HttpPut("blockuser")]
        public async Task<IActionResult> BlockUser(UserIdModel model)
        {
            if (_context.Users.Find(model.UserId) == null) return NotFound("User not found");
            _accountantService.BlockUser(model.UserId);
            return Ok("User Blocked");
        }

        [HttpPut("unblockuser")]
        public async Task<IActionResult> UnblockUser(UserIdModel model)
        {
            if (_context.Users.Find(model.UserId) == null) return NotFound("User not found");
            _accountantService.UnblockUser(model.UserId);
            return Ok("User Unblocked");
        }

        [HttpGet("anyuserloans")]
        public async Task<IActionResult> GetAnyUserLoans(UserIdModel model)
        {
            var userLoans = await _accountantService.GetAnyLoan(model.UserId);
            return Ok(userLoans);
        }
    }
}
