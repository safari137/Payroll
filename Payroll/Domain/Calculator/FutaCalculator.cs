using System;
using System.Linq;
using Payroll.DataContext;

namespace Payroll.Domain.Calculator
{
    public class FutaCalculator
    {
    	private decimal _federalUnemploymentTax;
    	
        public decimal Calculate(Employee employee, decimal paycheckGross)
        {
        	var grossYtd = this.GetEmployeeGross(employee.Id);
        	
            // No need to do any calculations if we've already paid the first income of 7,000
            if (grossYtd >= 7000)
                return 0;

            // Gross FederalUnemployment Remainder
            var grossFutaRemainder = 7000 - grossYtd;

            // This means we must pay Federal Unemployment on 100% of the income for this paycheck
            if (grossFutaRemainder >= paycheckGross)
                _federalUnemploymentTax = (paycheckGross * Global.FUTA_RATE);
            
            // We will only pay Federal Unemployment on a gross_futa_remainder
            else            
                _federalUnemploymentTax = (grossFutaRemainder * Global.FUTA_RATE);
            
            return _federalUnemploymentTax.Roundv2();
        }
        
        private decimal GetEmployeeGross(int employeeId)
        {
        	var beginDate = new DateTime(Settings.NextPayDate.Year, 1, 1);
        	var endDate = Settings.NextPayDate;
        	decimal gross = 0;

            using (var context = new PayrollContext())
            {
                var paycheckData = context.Paychecks
                    .Where(p => p.PayDate >= beginDate && p.PayDate <= endDate)
                    .Where(p => p.EmployeeId == employeeId)
                    .ToList();            

                foreach (var paycheck in paycheckData)
                    gross += paycheck.Gross;
            }
        	
        	return gross;        	
        }
    }
}
