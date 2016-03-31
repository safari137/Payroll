using System;
using Payroll.UI.Tools;

namespace Payroll.UI.GenericActivities
{
    public class GetDateActivity : DataEntryActivity
    {
        private string _input;
        
        public GetDateActivity(string message) : base(message)
        {
        	
        }

        protected override void ReceiveInput()
        {
            _input = Console.ReadLine();
        }

        protected override bool IsValid()
        {
            if (new DateValidation().IsValid(_input))
                return true;

            return false;
        }

        protected override void ProcessInput()
        {
            Result = _input;
        }
    }
}
