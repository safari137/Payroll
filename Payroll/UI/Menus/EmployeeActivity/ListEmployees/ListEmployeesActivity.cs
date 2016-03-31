using System;
using Payroll.Domain;
using Payroll.UI.GenericActivities;
using Payroll.UI.Tools;

namespace Payroll.UI.Menus.EmployeeActivity.ListEmployees
{
    class ListEmployeesActivity : Activity
    {
    	public ListEmployeesActivity()
    	{
    		Title = "List Employees";
    	}
        public override void Execute()
        {
        	using (new ConsoleColorSchemeChanger(ConsoleColor.Black, ConsoleColor.White))
            {
	            foreach (var employee in Employee.Employees)
	            {
	                Console.WriteLine("NAME    : {0}\tID      : {1}", employee.Name, employee.Id);
	                Console.WriteLine("DOB     : {0}\tSOC     : {1}", employee.DateOfBirth, employee.MaskSoc());
	                Console.WriteLine("PAY RATE: {0}\t\tHOURS   : {1}", employee.Wage, employee.Hours);
	                Console.WriteLine("--- Exemptions ---");
	                Console.WriteLine("Federal : {0}\tState: {1}", employee.FedExemptions, employee.StateExemptions);
	                Console.WriteLine();
	            }
        	}
        }
    }
}
