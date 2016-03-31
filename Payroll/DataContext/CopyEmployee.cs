using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Payroll.Domain;

namespace Payroll.DataContext
{
    public class CopyEmployee
    {
        public CopyEmployee()
        {

        }

        public void Transfer(Employee baseEmployee, ref Employee copiedEmployee)
        {
            copiedEmployee.Id = baseEmployee.Id;
            copiedEmployee.Name = baseEmployee.Name;
            copiedEmployee.IsSalary = baseEmployee.IsSalary;
            copiedEmployee.Hours = baseEmployee.Hours;
            copiedEmployee.DateOfBirth = baseEmployee.DateOfBirth;
            copiedEmployee.FedExemptions = baseEmployee.FedExemptions;
            copiedEmployee.Married = baseEmployee.Married;
            copiedEmployee.PaycycleYear = baseEmployee.PaycycleYear;
            copiedEmployee.SocialSecurityNumber = baseEmployee.SocialSecurityNumber;
            copiedEmployee.StateExemptions = baseEmployee.StateExemptions;
            copiedEmployee.Wage = baseEmployee.Wage;
            copiedEmployee.YearToDateIncome = baseEmployee.YearToDateIncome;         
        }
    }
}
