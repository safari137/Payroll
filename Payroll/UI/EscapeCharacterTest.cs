using System;

namespace Payroll
{
	public static class EscapeCharacterTest
	{
		private const char _escapeChar = '\\';
		
		public static bool IsEscape(string input)
		{
			if (input.Length == 1)
			{
				var enteredChar = Convert.ToChar(input);
				if (enteredChar == _escapeChar)
					return true;
			}
			
			return false;
		}
	}
}
