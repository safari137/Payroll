using System;
using System.Linq;
using Payroll.DataContext;
using Payroll.UI.GenericActivities;

namespace Payroll.UI.Menus.EmployeeActivity.PayEmployees
{
    public class ApproveChecksActivity : Activity
    {
        public ApproveChecksActivity()
        {
            Title = "Approve Checks";
        }

        public override void Execute()
        {
            Console.WriteLine("The following paychecks need your approval still.");
            
            using (var context = new PayrollContext())
            {
                var unapprovedPaychecks = context.Paychecks
                    .Where(p => p.Approved == false)
                    .ToList();

                foreach(var paycheck in unapprovedPaychecks)
                {
                    var employee = context.Employees.Single(e => e.Id == paycheck.EmployeeId);

                    new DisplayPaycheckConsole().Display(employee, paycheck);

                    var getApprovalActivity = new GetYesOrNoActivity("Approve this check? Y/N? >");
                    getApprovalActivity.Execute();

                    if (!(bool)getApprovalActivity.Result)
                        continue;

                    employee.YearToDateIncome += paycheck.Gross;
                    paycheck.Approved = true;
                }
                context.SaveChanges();
            }
        }
    }
}
