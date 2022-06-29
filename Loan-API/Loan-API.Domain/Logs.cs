using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_API.Domain
{
    public class Logs
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string MachineName { get; set; }
        public string Logger { get; set; }
    }
}
