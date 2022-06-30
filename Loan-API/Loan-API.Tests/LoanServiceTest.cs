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
using Loan_API.Models;

namespace Loan_API.Tests
{
    public class LoanServiceTest : IClassFixture<FakeLoanService>
    {
        FakeLoanService _service;
        public LoanServiceTest(FakeLoanService service)
        {
            _service = service;
        }

        [Fact]
        public void LoanShouldNotBeNull()
        {
            var model = new AddLoanModel() { Amount = 10, Currency = "USD", LoanPeriod = 10, LoanType = "Car Loan" };
            var userId = 1;

            var result = _service.AddLoan(model, userId);

            Assert.NotNull(result);
        }

        [Fact]
        public void CorrectLoanShouldBeDeleted()
        {
            var loanId = 1;
            var deletedLoan =_service.DeleteOwnLoan(loanId);

            Assert.Equal(deletedLoan.Id, loanId);
        }

        [Fact]
        public void GetLoanCountShouldBeOne()
        {
            var loanCount = 1;
            var fetchedLoans = _service.GetOwnLoans(1);

            Assert.Equal(fetchedLoans.Count(), loanCount);
        }

        [Fact]
        public void LoanShouldBeUpdated()
        {
            var model = new UpdateLoanModel() { Amount = 100, Currency = "GEL", LoanId = 1, LoanPeriod = 10, LoanType = "Car Loan" };
            var result = _service.UpdateOwnLoan(model).Result;
            Assert.Equal(model.Amount, result.Amount);
        }
    }
}
