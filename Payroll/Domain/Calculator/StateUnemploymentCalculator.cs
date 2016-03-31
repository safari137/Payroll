using System;
using System.Collections.Generic;
using System.Linq;
using Payroll.DataContext;
using Payroll.Domain.Payroll;

namespace Payroll.Domain.Calculator
{
    public class StateUnemploymentCalculator
    {
    	private decimal _stateUnemploymentTax;
    	
        public decimal Calculate(Employee employee, decimal gross)
        {
            var remainder = GetSuiExcessRemainder(employee);
            if (gross > remainder && remainder != -1)
                gross = remainder;

            _stateUnemploymentTax = (gross * Global.STATE_UNEMPLOYMENT_RATE);

            return _stateUnemploymentTax.Roundv2();
        }

        private static decimal GetSuiExcessRemainder(Employee employee)
        {
            decimal grossWages = 0;
            
            var dateRange = new DateRange(Settings.NextPayDate.Month, Settings.NextPayDate.Year);


            List<Paycheck> paycheckData;

            using (var context = new PayrollContext())
            {
                paycheckData = context.Paychecks
                    .Where(p => p.PayDate >= dateRange.BeginDate && p.PayDate <= dateRange.EndDate)
                    .Where(p => p.EmployeeId == employee.Id)
                    .ToList();
            }

            foreach (var paycheck in paycheckData)
            {
                grossWages += paycheck.Gross;
            }

            if (grossWages >= 8000)
                return 0;

            var remainder = (8000 - grossWages);

            return remainder;
        
        }

        private class DateRange
        {
            public DateTime BeginDate { get; private set; }
            public DateTime EndDate { get; private set; }

            public DateRange (int month, int year)
            {
                int quarterMonth = 0;

                if (month < 1 || month > 12)
                    throw new InvalidOperationException("Month is out of range");

                if (month > 0 && month <= 3)
                    quarterMonth = 1;
                else if (month > 3 && month <= 6)
                    quarterMonth = 4;
                else if (month > 6 && month <= 9)
                    quarterMonth = 6;
                else if (month > 9 && month <= 12)
                    quarterMonth = 10;

                BeginDate = new DateTime(year, quarterMonth, 1);
                EndDate = new DateTime(year, quarterMonth + 2, DateTime.DaysInMonth(year, quarterMonth + 2));                    
            }

            public DateRange GetDateRange()
            {
                return this;
            }
        }
    }
}
