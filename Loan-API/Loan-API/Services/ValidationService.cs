using FluentValidation;
using Loan_API;
using Loan_API.Data;
using Loan_API.Domain;
using Loan_API.Models;
using System.Linq;

namespace Loan_API.Services
{
    public class UserValidator : AbstractValidator<RegistrationModel>
    {
        UserContext _context;
        public UserValidator(UserContext context)
        {
            _context = context;

            RuleFor(RegistrationModel => RegistrationModel.FirstName)
                .Length(1, 50).WithMessage("First Name must containt 1-50 characters")
                .NotNull().WithMessage("First Name is mandatory")
                .Matches(@"^[a-zA-Z]+$").WithMessage("First Name must only contain letters");
            RuleFor(RegistrationModel => RegistrationModel.LastName)
                .Length(1, 50).WithMessage("Last Name must containt 1-50 characters")
                .NotNull().WithMessage("Last Name is mandatory")
                .Matches(@"^[a-zA-Z]+$").WithMessage("Last Name must only contain letters");
            RuleFor(RegistrationModel => RegistrationModel.UserName)
                .Length(1, 50).WithMessage("Last Name must containt 1-50 characters")
                .NotNull().WithMessage("Last Name is mandatory")
                .Matches(@"^[a-zA-Z0-9]+$").WithMessage("Username must contain only letters or numbers")
                .Must(UniqueUserName).WithMessage("Username already exists");
        }

        private bool UniqueUserName(string name)
        {
            var uniqueCheck = _context.Users.Where(x => x.UserName.ToLower() == name.ToLower()).FirstOrDefault();

            if (uniqueCheck == null) return true;
            return false;
        }
    }
}
