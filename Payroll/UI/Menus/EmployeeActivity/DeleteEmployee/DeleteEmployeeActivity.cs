using System;
using System.Linq;
using Payroll.DataContext;
using Payroll.Domain;
using Payroll.UI.GenericActivities;
using Payroll.UI.Tools;

namespace Payroll.UI.Menus.EmployeeActivity.DeleteEmployee
{
    public class DeleteEmployeeActivity : Activity
    {
        private Employee _employee;

        public DeleteEmployeeActivity()
        {
            Title = "Delete Employee";
        }

        public override void Execute()
        {              
            using (new ConsoleColorSchemeChanger(ConsoleColor.Black, ConsoleColor.White))
            {
            	// Get Id
	            var getId = new GetEmployeeIdActivity();
	            getId.Execute();
	            this._employee = (Employee)getId.Result;
	
	            // Confirm Deletion
	            Console.WriteLine("Employee: {0}\tID:{1}", _employee.Name, _employee.Id);
	            var confirmDeletion = new GetYesOrNoActivity("Delete employee? Y/N? >");
	            confirmDeletion.Execute();
	            
	            if ((bool)confirmDeletion.Result == true)
	            {           
				    using (var payrollContext = new PayrollContext())
                    {
                        var employeeToRemove = payrollContext.Employees.Single(e => e.Id == _employee.Id);
                        payrollContext.Employees.Remove(employeeToRemove);
                        payrollContext.SaveChanges();
                    }
	            }
	            else
	            	Console.WriteLine("Delete Canceled.");
            }
        }
    }
}
