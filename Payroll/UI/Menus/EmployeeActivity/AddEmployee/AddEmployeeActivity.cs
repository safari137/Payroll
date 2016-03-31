using System;
using Payroll.DataContext;
using Payroll.Domain;
using Payroll.UI.GenericActivities;
using Payroll.UI.Tools;

namespace Payroll.UI.Menus.EmployeeActivity.AddEmployee
{
    public class AddEmployeeActivity : DataEntryActivity
    {
        private Employee _employee;

        public AddEmployeeActivity()
        {
            Title = "Add Employee";
        }

        protected override void DisplayMessage()
        {
        	using (new ConsoleColorSchemeChanger(ConsoleColor.Black, ConsoleColor.White))
            {
	            // Get Name
	            var nameActivity = new GetStringActivity("Enter Name >");
	            nameActivity.Execute();
	            var name = (string)nameActivity.Result;
	
	            // Get Date Of Birth
	            var dobActivity = new GetDateActivity("Enter Date of Birth >");
	            dobActivity.Execute();
	            var dateOfBirth = (string)dobActivity.Result;
	
	            // Get Social Security Number
	            var socActivity = new GetSocActivity();
	            socActivity.Execute();
	            var socialSecurityNumber = (string)socActivity.Result;
	
	            // Get Salary
	            var getSalaryActivity = new GetDecimalActivity("Enter Salary >");
	            getSalaryActivity.Execute();
	            var wage = (decimal)getSalaryActivity.Result;
	
	            // Is the employee Hourly or Salary?
	            var isSalaryActivity = new GetYesOrNoActivity("Salary Employee? Y/N? >");
	            isSalaryActivity.Execute();
	            var isSalary = (bool)isSalaryActivity.Result;
	
	            // Get Federal Exemptions
	            var fedExemptionActivity = new GetFederalExemptionActivity();
	            fedExemptionActivity.Execute();
	            var fedExemptions = (int)fedExemptionActivity.Result;            
	
	            // Get State Exemptions
	            var stateExemptionActivity = new GetStateExemptionActivity();
	            stateExemptionActivity.Execute();
	            var stateExemptions = (int)stateExemptionActivity.Result;
	
	            // Is the employee married?
	            var isMarriedActivity = new GetYesOrNoActivity("Married? Y/N? >");
	            isMarriedActivity.Execute();
	            var married = (bool)isMarriedActivity.Result;
	
	            var paycycleYear = DateTime.Today.Year;
	
	            _employee = new Employee(socialSecurityNumber, paycycleYear, married, name, dateOfBirth, wage, 0M, 0M, isSalary,
	                fedExemptions, stateExemptions);
        	}
        }

        protected override void ReceiveInput()
        {
            
        }

        protected override bool IsValid()
        {
            return true;
        }

        protected override void ProcessInput()
        {
            Result = false;
            using(var payrollContext = new PayrollContext())
            {
                payrollContext.Employees.Add(this._employee);
                payrollContext.SaveChanges();
                Result = true;

                Console.WriteLine();
                Console.WriteLine("Employee Added Successfully.");
            }            

            if (!(bool)Result)
            {
                Console.WriteLine("Employee Add Failed!");
            }
        }
    }
}
