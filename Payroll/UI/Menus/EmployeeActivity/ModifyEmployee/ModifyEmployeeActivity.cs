using System;
using System.Linq;
using Payroll.DataContext;
using Payroll.Domain;
using Payroll.UI.GenericActivities;
using Payroll.UI.Tools;

namespace Payroll.UI.Menus.EmployeeActivity.ModifyEmployee
{
    public class ModifyEmployeeActivity : Activity
    {
        public ModifyEmployeeActivity()
        {
            Title = "Modify Employee";
        }
        public override void Execute()
        {
        	using (new ConsoleColorSchemeChanger(ConsoleColor.Black, ConsoleColor.White))
            {
	            var getIdActivity = new GetEmployeeIdActivity();
	            getIdActivity.Execute();
	            var employee = (Employee)getIdActivity.Result;
	
	            var getModification = new GetModificationStringActivity(employee);
	            getModification.Execute();
	            employee = (Employee)getModification.Result;
	
	            using (var context = new PayrollContext())
                {
                    var savedEmployee = context.Employees.Single(e => e.Id == employee.Id);
                    new CopyEmployee().Transfer(employee, ref savedEmployee);
                    context.SaveChanges();
                }
        	}
        }
    }
}
