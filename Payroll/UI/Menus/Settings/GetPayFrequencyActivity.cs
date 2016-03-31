using System;
using Payroll.UI.GenericActivities;
using Payroll.UI.Tools;

namespace Payroll.UI.Menus.Settings
{
	public class GetIntegerActivity : DataEntryActivity
	{
		private string _input;
		private int _number, _min, _max;
		
		public GetIntegerActivity(int min, int max, string message) : base(message)
		{
			this._min = min;
			this._max = max;
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
			
			_number = int.Parse(_input);
			
			if (_number > this._max || _number < this._min)
				return false;
			
			return true;
		}
		
		protected override void ProcessInput()
		{
			Result = _number;
		}
	}
}
