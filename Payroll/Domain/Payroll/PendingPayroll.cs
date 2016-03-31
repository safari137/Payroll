using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Payroll
{
	public class PendingPayroll
	{
		private readonly List<Paycheck> _pendingPaychecks = new List<Paycheck>();
		
		private IDisplay _display;

		public PendingPayroll(IDisplay display)
		{
			this._display = display;
		}

		public void AddPaycheck(Paycheck paycheck)
		{
			this._pendingPaychecks.Add(paycheck);
		}

		public bool ApproveChecks()
		{
			bool _oneAtATime = false;

			Console.WriteLine("Would you like to approve one at a time?");
			Console.WriteLine("Selecting no approves as a batch.");
			Console.Write("y/n >");
			var keyPressed = Console.ReadKey();

			if (keyPressed.Key == ConsoleKey.Y)
				_oneAtATime = true;

			for (int i = _pendingPaychecks.Count - 1; i >= 0; i--)
			{				
				var paycheck = _pendingPaychecks[i];
				var employee = Employee.Find(paycheck.EmployeeId);
			
				_display.Display(employee, paycheck);

				if (_oneAtATime)
				{
					Console.WriteLine("Selecting no will remove the paycheck.");
					Console.Write("Approve? y/n >");
					keyPressed = Console.ReadKey();
					if (keyPressed.Key == ConsoleKey.Y)
						_pendingPaychecks.RemoveAt(i);
				}
			}

			if (!_oneAtATime)
			{
				Console.WriteLine("Selecting no will remove the paycheck.");
				Console.Write("Approve? y/n >");
				keyPressed = Console.ReadKey();
				if (keyPressed.Key == ConsoleKey.N)
					_pendingPaychecks.Clear();
			}

			return this.Finalize();
		}

		private bool Finalize()
		{
			var billTracker = new BillTracker();
			
			if (_pendingPaychecks.Count < 1)
				return false;

			foreach(var paycheck in _pendingPaychecks)
			{
				var employee = Employee.Find(paycheck.EmployeeId);
				
				employee.YearToDateIncome += paycheck.Gross;
			}

			return true;
		}
	}
}
