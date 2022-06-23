using FluentValidation;
using Loan_API;
using Loan_API.Data;
using Loan_API.Domain;
using Loan_API.Models;
using System.Collections.Generic;
using System.Linq;


namespace Loan_API.Services
{

    public class LoanValidator : AbstractValidator<AddLoanModel>
    {
        UserContext _context;
        public LoanValidator(UserContext context)
        {
            _context = context;

            RuleFor(AddLoanModel => AddLoanModel.LoanType)
                .NotEmpty().NotNull().WithMessage("Loan Type is required")
                .Must(CheckLoanType).WithMessage($"Invalid Loan Type. Loan  must be one of the following: {LoanType.QuickLoan}," +
                                                                                                        $"{LoanType.CarLoan}" +
                                                                                                        $"{LoanType.Installment}");

                





        }
        private bool CheckLoanType(string loanType)
        {
            List<string> loanTypes = new List<string>()
                {
                    LoanType.QuickLoan.ToLower(),
                    LoanType.CarLoan.ToLower(),
                    LoanType.Installment.ToLower()
                };
            var loanTypeLower = loanType.ToLower();
            if (loanTypes.Contains(loanTypeLower)) return true;
            else return false;
        }
    }
}
