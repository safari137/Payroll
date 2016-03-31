using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Payroll.Domain;
using Payroll.Domain.Bills;
using Payroll.Domain.Payroll;

namespace Payroll.DataContext
{
    public class PayrollContext : DbContext
    {
        public DbSet<Paycheck> Paychecks { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Bill> Bills { get; set; }
    }
}
