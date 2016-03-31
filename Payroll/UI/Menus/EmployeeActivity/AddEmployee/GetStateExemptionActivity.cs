using System;
using Payroll.UI.GenericActivities;
using Payroll.UI.Tools;

namespace Payroll.UI.Menus.EmployeeActivity.AddEmployee
{
    public class GetStateExemptionActivity : DataEntryActivity
    {
        private string _input;
        private int _exemptions;



        protected override void DisplayMessage()
        {
            Console.Write("Enter State Exemptions >");
        }

        protected override void ReceiveInput()
        {
            _input = Console.ReadLine();
        }

        protected override bool IsValid()
        {
            var numberValidation = new NumberValidation();

            if (!numberValidation.IsValid(_input))
                return false;

            _exemptions = int.Parse(_input);

            if (_exemptions < 0 || _exemptions > 9)
                return false;

            return true;
        }

        protected override void ProcessInput()
        {
            Result = _exemptions;
        }
    }
}
