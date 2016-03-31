using System;

namespace Payroll.UI.GenericActivities
{
	public class GetStringActivity : DataEntryActivity
    {
        private string _input;

        public GetStringActivity(string message) : base(message)
		{
			
		}

        protected override void ReceiveInput()
        {
            _input = Console.ReadLine();
        }

        protected override bool IsValid()
        {
            if (_input.Length > 20)
                return false;

            return true;
        }

        protected override void ProcessInput()
        {
            Result = (object)_input;
        }
    }
}
