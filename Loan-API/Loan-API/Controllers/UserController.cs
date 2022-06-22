﻿using Microsoft.AspNetCore.Mvc;
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
//using Week17.Helpers;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Loan_API.Models;

namespace Loan_API.Controllers
{
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
    }


}