using System;

namespace Payroll.UI.GenericActivities
{
    public class GetDecimalActivity : DataEntryActivity
    {
        private string _input;
        private decimal _amount;

        public GetDecimalActivity(string message) : base(message)
		{
			
		}

        protected override void ReceiveInput()
        {
            _input = Console.ReadLine();
        }

        protected override bool IsValid()
        {
            var result = decimal.TryParse(_input, out _amount);

            return result;
        }

        protected override void ProcessInput()
        {
            Result = _amount;
        }
    }
}
