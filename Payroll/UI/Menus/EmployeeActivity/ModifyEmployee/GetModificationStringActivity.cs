using System;
using Payroll.Domain;
using Payroll.UI.GenericActivities;
using Payroll.UI.Tools;

namespace Payroll.UI.Menus.EmployeeActivity.ModifyEmployee
{
    public class GetModificationStringActivity : DataEntryActivity
    {
        private string _input;
        private Employee _employee;

        public GetModificationStringActivity(Employee employee)
        {
            Title = "Modify Employee";
            this._employee = employee;
        }

        protected override void DisplayMessage()
        {
            Console.WriteLine("[FIELD] = NAME, SOC, PAYRATE, DOB, FEDEXEMPT OR STATEEXEMPT");
            Console.WriteLine("Syntax: [FIELD]=[VALUE]");
            Console.WriteLine("Example: DOB=10/14/1990");
            Console.Write(">");
        }

        protected override void ReceiveInput()
        {
            this._input = Console.ReadLine();
        }

        protected override bool IsValid()
        {
        	bool success, formatValidity;
        	int exemptions;
        	decimal wage;
        	
            var index = _input.IndexOf('=');

            if (index < 0)
                return false;
          
            var field = _input.Substring(0, index);
            var value = _input.Substring(index + 1);

            switch (field)
            {
                case "SOC":
                    _employee.SocialSecurityNumber = value;
                    success = true;
                    break;

                case "DOB":
                    success = new DateValidation().IsValid(value);
                    _employee.DateOfBirth = (success) ? value : _employee.DateOfBirth;
                    break;

                case "NAME":
                    _employee.Name = value;
                    success = true;
                    break;

                case "PAYRATE":
                    formatValidity = decimal.TryParse(value, out wage);
                    _employee.Wage = (formatValidity) ? wage : _employee.Wage;
                    success = formatValidity;
                    break;
                    
                case "FEDEXEMPT":
                    formatValidity = int.TryParse(value, out exemptions);
                    _employee.FedExemptions = (formatValidity) ? exemptions : _employee.FedExemptions;
                    success = formatValidity;
                    break;
                    
                case "STATEEXEMPT":
                    formatValidity = int.TryParse(value, out exemptions);
                    _employee.StateExemptions = (formatValidity) ? exemptions : _employee.StateExemptions;
                    success = formatValidity;
                    break;

                default:
                    success = false;
                    break;
            }
            return success;
        }

        protected override void ProcessInput()
        {
            Result = _employee;
        }
    }
}
