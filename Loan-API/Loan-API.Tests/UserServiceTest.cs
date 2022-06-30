using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Xunit;
using Moq;
using Loan_API.Controllers;
using Loan_API.Services;
using Loan_API.Domain;
using Loan_API.Data;
using Microsoft.Extensions.Options;
using Loan_API.Helpers;
using System.Collections.Generic;
using MockQueryable.Moq;
using Loan_API.Tests.FakeServices;

namespace Loan_API.Tests
{
    public class UserServiceTest : IClassFixture<FakeUserService>
    {
        FakeUserService _service;
        public UserServiceTest(FakeUserService service)
        {
            _service = service;
        }

        [Fact]
        public void ShouldBeNullIfCredsAreNull()
        {
            var username = "";
            var password = "";

            var result = _service.Authenticate(username, password);
            Assert.Null(result);
        }
    }
}
