using System.Linq;
using Payroll.DataContext;
using Payroll.DataFlatFile;
using Payroll.Domain.Bills.BillTracker;
using Payroll.Domain.Calculator;

namespace Payroll.Domain.Payroll.Engine
{

    public class PayrollEngine
    {         
        private readonly ITaxCalculator _taxCalculator;

        private readonly BillTracker _billTracker = new BillTracker();

        public PayrollEngine(ITaxCalculator taxCalculator)
        {
        	this._taxCalculator = taxCalculator;
        }
        
        public void Start()
        {
            using (var payrollContext = new PayrollContext())
            {
                var paylist = payrollContext.Employees
                    .Where(e => e.IsSalary || (e.Wage > 0 && e.Hours > 0))
                    .ToList();

                foreach(var employee in paylist)
                {
                    var processor = new PaycheckProcessor(employee, _taxCalculator);

                    var currentPaycheck = processor.ProcessedPaycheck;                
                    
                    employee.YearToDateIncome += currentPaycheck.Gross;
                    employee.Hours = 0M;

            	    payrollContext.Paychecks.Add(currentPaycheck);
                    _billTracker.AppendBill(currentPaycheck);
                }

                // Settings
                Settings.NextPayDate = (Settings.NextPayDate.AddDays(14));
                Settings.CloseDate = (Settings.CloseDate.AddDays(14));

                // Save Data            
                payrollContext.SaveChanges();
            }
            
            Data.SaveSettings();              
        }
    }
}
