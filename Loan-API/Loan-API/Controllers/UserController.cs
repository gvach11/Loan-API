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
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;
        private IUserService _userService;
        public UserController(UserContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        
        //Register
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<User>> AddUser(RegistrationModel regData)
        {
            UserValidator validator = new UserValidator(_context);
            ValidationResult result = validator.Validate(regData);
            if (!result.IsValid)
            {
                return BadRequest(validator.GetErrors(result));
            }
            _userService.Register(regData);
            await _context.SaveChangesAsync();
            return Ok("Registration Successful");
        }

        //Login
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            var user = _userService.Authenticate(username, password);
            if (user == null) return BadRequest("Username or Password incorrect");
            _userService.Login(user);
            return Ok($"Login Successful. Your token: {user.Token}");
        }
    }


}
