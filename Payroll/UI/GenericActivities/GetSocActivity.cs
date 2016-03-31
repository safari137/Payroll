using System;

namespace Payroll.UI.GenericActivities
{
    public class GetSocActivity : DataEntryActivity
    {
        private string _input;

        protected override void DisplayMessage()
        {
            Console.Write("Enter Social Security Number >");
        }

        protected override void ReceiveInput()
        {
            _input = Console.ReadLine();
        }

        protected override bool IsValid()
        {
            if (_input.Length > 11)
                return false;
            else
                return true;
        }

        protected override void ProcessInput()
        {
            Result = _input;
        }
    }
}
