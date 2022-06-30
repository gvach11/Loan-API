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
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Loan_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;
        private IUserService _userService;
        private readonly ILogger<UserController> _logger;
        public UserController(UserContext context, IUserService userService, ILogger<UserController> logger)
        {
            _context = context;
            _userService = userService;
            _logger = logger;
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
                foreach (var error in ValidationErrorParse.GetErrors(result))
                {
                    _logger.LogError(error);
                }
                return BadRequest(ValidationErrorParse.GetErrors(result));
                
            }
            _userService.Register(regData);
            await _context.SaveChangesAsync();
            return Ok("Registration Successful");
        }

        //Login
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(LoginModel model)
        {
            var user = _userService.Authenticate(model.UserName, model.Password);
            if (user == null) {
                _logger.LogError("Username or Password incorrect");
                return BadRequest("Username or Password incorrect"); }
            _userService.Login(user);
            return Ok($"Login Successful. Your token: {user.Token}");
        }

        //Own Data
        [HttpGet("owndata")]
        public IActionResult GetOwnData()
        {
            var userData = _userService.GetOwnData();
            return Ok(userData);
        }
    }


}
