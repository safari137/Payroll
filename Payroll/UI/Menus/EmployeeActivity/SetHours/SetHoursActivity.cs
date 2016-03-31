using System;
using System.Linq;
using Payroll.DataContext;
using Payroll.Domain;
using Payroll.UI.GenericActivities;
using Payroll.UI.Tools;

namespace Payroll.UI.Menus.EmployeeActivity.SetHours
{
    public class SetHoursActivity : Activity
    {
        public SetHoursActivity()
        {
            Title = "Set Hours";
        }
        public override void Execute()
        {
        	using (new ConsoleColorSchemeChanger(ConsoleColor.Black, ConsoleColor.White))
            {
	            // Get Employee Id
	            var getIdActivity = new GetEmployeeIdActivity();
	            getIdActivity.Execute();
	            var employee = (Employee)getIdActivity.Result;
	
	            // Get Hours
	            var getHoursActivity = new GetHoursActivity();
	            getHoursActivity.Execute();
	            employee.Hours = (decimal)getHoursActivity.Result;
	
	            using (var context = new PayrollContext())
                {
                    var databaseEmployee = context.Employees.Single(e => e.Id == employee.Id);
                    databaseEmployee.Hours = employee.Hours;
                    context.SaveChanges();
                }
        	}
        }
    }
}
