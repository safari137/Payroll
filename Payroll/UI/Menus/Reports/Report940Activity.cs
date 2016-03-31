using System;
using Payroll.UI.GenericActivities;
using Payroll.UI.Reports;
using Payroll.UI.Tools;

namespace Payroll.UI.Menus.Reports
{
	public class Report940Activity : Activity
	{
		private DateTime _beginningDate, _endDate;
		
		public Report940Activity()
		{
			Title = "Report 940";
		}
		
		public override void Execute()
		{
			using (new ConsoleColorSchemeChanger(ConsoleColor.Black, ConsoleColor.White))
            {
				var getBeginningDate = new GetDateActivity("Enter beginning date >");
				getBeginningDate.Execute();
				var beginDateString = (string)getBeginningDate.Result;
				_beginningDate = Convert.ToDateTime(beginDateString);
				
				var getEndDate = new GetDateActivity("Enter ending date >");
				getEndDate.Execute();
				var endDateString = (string)getEndDate.Result;
				_endDate = Convert.ToDateTime(endDateString);
				
				var report = new Report940(_beginningDate, _endDate);
				report.Display();
			}
		}
	}
}
