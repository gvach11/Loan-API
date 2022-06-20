using System;
using Microsoft.EntityFrameworkCore;
using Loan_API.Domain;

namespace Loan_API.Data
{
    public class UserContext : DbContext
    {
        public UserContext()
        {
        }

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Accountant> Accountants { get; set; }
        public DbSet<Loan> Loans { get; set; }
    }
}
