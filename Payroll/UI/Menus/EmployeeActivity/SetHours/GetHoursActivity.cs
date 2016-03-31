using System;
using Payroll.UI.GenericActivities;
using Payroll.UI.Tools;

namespace Payroll.UI.Menus.EmployeeActivity.SetHours
{
    public class GetHoursActivity : DataEntryActivity
    {
        private string _input;
        private decimal _hours;

        protected override void DisplayMessage()
        {
            Console.Write("Enter Hours >");
        }

        protected override void ReceiveInput()
        {
            _input = Console.ReadLine();
        }

        protected override bool IsValid()
        {
            var numberValidation = new NumberValidation();

            var isDecimal = decimal.TryParse(_input, out _hours);

            if (!isDecimal)
                return false;

            if (_hours < 0)
                return false;

            return true;
        }

        protected override void ProcessInput()
        {
            Result = _hours;
        }
    }
}
