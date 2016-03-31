using System;
using System.Linq;
using Payroll.DataContext;
using Payroll.Domain.Calculator;
using Payroll.Domain.Payroll.Engine;
using Payroll.UI.GenericActivities;

namespace Payroll.UI.Menus.EmployeeActivity.PayEmployees
{
	public class PayEmployeesActivity : Activity
	{
		public PayEmployeesActivity()
		{
			Title = "Pay Employees";			
		}
		public override void Execute()
		{
            bool proceed =  false;
            using (var context = new PayrollContext())
            {
                var unapprovedChecks = context.Paychecks
                    .Where(p => p.Approved == false)
                    .ToList();

                if (unapprovedChecks.Count > 0)
                {
                    Console.WriteLine("You have unapproved paychecks pending.");
                    Console.WriteLine("Please approve or delete them before running payroll.");
                }
                else
                    proceed = true;
            }

            if (!proceed)
                return;
            

			var payrollEngine = new PayrollEngine(new TaxCalculator2015());
			
			payrollEngine.Start();

            new ApproveChecksActivity().Execute();            
		}
	}
}
