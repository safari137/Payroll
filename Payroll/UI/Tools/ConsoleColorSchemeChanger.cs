using System;

namespace Payroll.UI.Tools
{
	public class ConsoleColorSchemeChanger : IDisposable
	{
		private ConsoleColor _previousForeGroundColor;
		private ConsoleColor _previousBackGroundColor;
		
		public ConsoleColorSchemeChanger(ConsoleColor foreGroundColor, ConsoleColor backGroundColor)
		{
			this._previousForeGroundColor = Console.ForegroundColor;
			this._previousBackGroundColor = Console.BackgroundColor;
			
			Console.ForegroundColor = foreGroundColor;
			Console.BackgroundColor = backGroundColor;
		}
		
		public void Revert()
		{
			Console.ForegroundColor = _previousForeGroundColor;
			Console.BackgroundColor = _previousBackGroundColor;
		}
		
		public void Dispose()
		{
			this.Revert();
		}
	}
}
