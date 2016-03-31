using System.Linq;
using Payroll.DataContext;
using Payroll.DataFlatFile;
using Payroll.Domain.Bills.BillTracker;
using Payroll.Domain.Calculator;

namespace Payroll.Domain.Payroll.Engine
{

    public class PayrollEngine
    {         
        private ITaxCalculator _taxCalculator;

        private BillTracker _billTracker = new BillTracker();

        public PayrollEngine(ITaxCalculator taxCalculator)
        {
        	this._taxCalculator = taxCalculator;
        }
        
        public void Start()
        {
            using (var context = new PayrollContext())
            {
                var paylist = context.Employees
                    .Where(e => e.IsSalary || (e.Wage > 0 && e.Hours > 0))
                    .ToList();

                foreach(var employee in paylist)
                {
                    var processor = new PaycheckProcessor(employee, _taxCalculator);

                    var currentPaycheck = processor.ProcessedPaycheck;                
                    
                    employee.YearToDateIncome += currentPaycheck.Gross;
                    employee.Hours = 0M;

            	    context.Paychecks.Add(currentPaycheck);
                    _billTracker.AppendBill(currentPaycheck);
                }

                // Settings
                Settings.NextPayDate = (Settings.NextPayDate.AddDays(14));
                Settings.CloseDate = (Settings.CloseDate.AddDays(14));

                // Save Data            
                context.SaveChanges();
            }
            
            Data.SaveSettings();              
        }
    }
}
