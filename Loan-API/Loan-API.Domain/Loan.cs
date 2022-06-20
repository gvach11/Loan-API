using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_API.Domain
{
    public class Loan
    {
        public Loan()
        {
            Status = LoanStatus.Processing;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public TimeSpan Period { get; set; }
        public User User { get; set; }

        
    }

    public class LoanStatus
    {
        public const string Processing = "Processing";
        public const string Approved = "Approved";
        public const string Declined = "Declined";
    }

    public class LoanType
    {
        public const string QuickLoan = "Quick Loan";
        public const string CarLoan = "Car Loan";
        public const string Installment = "Installment";
    }

    public class LoanCurrency
    {
        public const string USD = "USD";
        public const string EUR = "EUR";
        public const string GEL = "GEL";
    }
}
