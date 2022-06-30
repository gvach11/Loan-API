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
using Microsoft.AspNetCore.Identity;

namespace Loan_API.Helpers
{
    public interface ITokenParse
    {
        public int GetUserId();
    }
    public class TokenParse : ITokenParse
    {
        private readonly IHttpContextAccessor _httpContext;

        public TokenParse(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public int GetUserId()
        {
            var user = _httpContext?.HttpContext?.User as ClaimsPrincipal;
            var userId = user.Claims.ElementAt(0).Value;
            var userIdInt = Convert.ToInt32(userId);
            return userIdInt;
        }
    }


}
