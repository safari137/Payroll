using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Payroll.Calculator;

namespace Payroll.UI
{
    public class EmployeeUI : MenuBase
    {
        public EmployeeUI(int line) : base("Employee Menu", line)
        {
            base.Register(new MenuItem(MenuItemType.Option, 1, "Add Employee", this.AddEmployee));
            base.Register(new MenuItem(MenuItemType.Option, 2, "Delete Employee", this.DeleteEmployee));
            base.Register(new MenuItem(MenuItemType.Option, 3, "List Employees", this.ListEmployees));
            base.Register(new MenuItem(MenuItemType.Option, 4, "Set Hours", this.SetHours));
            base.Register(new MenuItem(MenuItemType.Option, 5, "Inactivate Employee", this.InactivateEmployee));
            base.Register(new MenuItem(MenuItemType.Option, 6, "Modify Employee", this.ModifyEmployee));
            base.Register(new MenuItem(MenuItemType.Option, 7, "Pay Employees", this.PayEmployees));
            base.Register(new TimeManagerUI(8).Deliver());
        }

        private void PayEmployees()
        {
        	var payrollEngine = new PayrollEngine(new TaxCalculator2015());

            // Pay the employees
            payrollEngine.Start();
        }

        private void AddEmployee()
        {
            var employee = new Employee();
            employee.SetupInterview();
        }

        private void DeleteEmployee()
		{
			Console.Write("Enter Employee ID:>");
			var id = Int32.Parse (Console.ReadLine ());
	
			var employee = Employee.Find(id);
	
			if (employee == null) 
			{
				Console.WriteLine ("Employee: {0} : not found.", id);
				return;
			}
				
			Console.Write("Delete: {0}\tID:{1} ? y/n >", employee.Name, employee.ID);
			var confirmationKey = Console.ReadKey();
				
			// Create New Line
			Console.WriteLine();
				
			if(confirmationKey.Key == ConsoleKey.N)
			{
				Console.WriteLine("Aborting.");
				return;
			}
			if (confirmationKey.Key == ConsoleKey.Y)
			{
				Employee.Employees.Remove(employee);
	            Data.SaveEmployeeFile();
				return;
			}
			Console.WriteLine("Answer not recognized.  Aborting.");
				
		}
	
		private void ListEmployees()
		{
			if (Employee.Employees == null)
			{
				Console.WriteLine("No Employees found.");
				return;
			}
			foreach (Employee employee in Employee.Employees) 
			{
				Console.WriteLine ("NAME    : {0}\tID      : {1}", employee.Name, employee.ID);
				Console.WriteLine ("DOB     : {0}\tSOC     : {1}", employee.DateOfBirth, employee.MaskSoc());
				Console.WriteLine ("PAY RATE: {0}\t\tHOURS   : {1}", employee.Wage, employee.Hours);
                Console.WriteLine("--- Exemptions ---");
                Console.WriteLine ("Federal : {0}\tState: {1}", employee.FedExemptions, employee.StateExemptions);
				Console.WriteLine ();
			}
		}
	
		private void SetHours()
		{
			int id;
			Employee employee;
            decimal hours;

			Console.Write("Enter Employee ID:>");

            try
            {

                id = Int32.Parse(Console.ReadLine());

                employee = Employee.Find(id);

                if (employee == null)
                {
                    Console.WriteLine("Employee: {0} : not found.", id);
                    return;
                }


                Console.Write("Would you like to pick hours from TimeTask data? y/n >");
                var pressedKey = Console.ReadKey();
                if (pressedKey.Key == ConsoleKey.Y)
                {
                    hours = this.GetHours(id);

                    Console.WriteLine();

                    Console.WriteLine("Hours = {0}", hours);
                    Console.WriteLine("Is this correct? y/n >");

                    pressedKey = Console.ReadKey();
                    if (pressedKey.Key == ConsoleKey.N)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Please modify data in the Time Manager menu.");
                        return;
                    }
                    else
                        employee.Hours = hours;
                }
                else
                {
                    Console.Write("Enter Hours >");
                    employee.Hours = decimal.Parse(Console.ReadLine());
                }

                Data.SaveEmployeeFile();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
		}
			
		private void InactivateEmployee()
		{
			int id;
				
			Console.Write("Enter ID >");

            try
            {
                id = Int32.Parse(Console.ReadLine());
                var employee = Employee.Find(id);

                if (employee == null)
                {
                    Console.WriteLine("Employee not found!");
                    return;
                }

                Console.WriteLine("Inactivate: {0} ({1})? y/n", employee.Name, employee.ID);

                ConsoleKeyInfo confirmationKey = Console.ReadKey();

                if (confirmationKey.Key == ConsoleKey.N)
                    return;
                if (confirmationKey.Key == ConsoleKey.Y)
                {
                    employee.Wage = -1m;
                    Console.Write("Employee Inactivated.");
                }
            }
            finally
            {
                Data.SaveEmployeeFile();
            }
				
			return;
				
		}
			
		private void ModifyEmployee()
		{
			string field, value;
				
			Console.Write("Enter ID >");
			int id = Int32.Parse(Console.ReadLine());
			var employee = Employee.Find(id);
				
			if (employee == null)
			{
				Console.WriteLine("ID not found!");
				return;
			}
				
			Console.WriteLine("[FIELD] = NAME, SOC, PAYRATE, DOB FEDEXEMPT OR STATEEXEMPT");
			Console.WriteLine("Syntax: [FIELD]=[VALUE]");
			Console.WriteLine("Example: DOB=10/14/1990");
			Console.Write(">");
				
			var command = Console.ReadLine();
				
			var index = command.IndexOf('=');
				
			if (index < 0)
			{
				Console.WriteLine("Incorrect Format!");
				return;
			}
				
			field = command.Substring(0, index);
			value = command.Substring(index + 1);
				
			switch (field)
			{
				case "SOC":
					employee.SocialSecurityNumber = value;
					break;
				case "DOB":
					employee.DateOfBirth = value;
					break;
				case "NAME":
					employee.Name = value;
					break;
				case "PAYRATE":
					employee.Wage = decimal.Parse(value);
					employee.Wage = employee.Wage.Roundv2();
					break;
                case "FEDEXEMPT":
                    employee.FedExemptions = Int32.Parse(value);
                    break;
                case "STATEEXEMPT":
                    employee.StateExemptions = Int32.Parse(value);
                    break;
				default:
					Console.WriteLine("Field not recognized!");
					break;
			}
            Data.SaveEmployeeFile();	
		}

        private decimal GetHours(int employeeId)
        {
            var beginDate = new DateTime(Settings.CloseDate.Year, Settings.CloseDate.Month, Settings.CloseDate.Day);
            var endDate = new DateTime(Settings.CloseDate.Year, Settings.CloseDate.Month, Settings.CloseDate.Day);
            decimal totalHours = 0M;

            beginDate = beginDate.AddDays(-17);
            endDate = endDate.AddDays(-3);

            var timeManager = Data.LoadTasks(beginDate, endDate);

            var timeQuery = timeManager.GetTasks(employeeId);

            foreach (var timeData in timeQuery)
                totalHours += timeData.Hours;

            return totalHours;
        }
	}
}
