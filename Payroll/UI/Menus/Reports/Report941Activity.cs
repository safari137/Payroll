using System;
using Payroll.UI.GenericActivities;
using Payroll.UI.Reports;
using Payroll.UI.Tools;

namespace Payroll.UI.Menus.Reports
{
	public class Report941Activity : Activity
	{
		public Report941Activity()
		{
			Title = "Report 941";
		}
		public override void Execute()
		{
			using (new ConsoleColorSchemeChanger(ConsoleColor.Black, ConsoleColor.White))
            {
				var getBeginningDate = new GetDateActivity("Enter Beginning Date >");
				getBeginningDate.Execute();
				var beginningDateString = (string)getBeginningDate.Result;
				var beginningDate = Convert.ToDateTime(beginningDateString);
				
				var getEndDate = new GetDateActivity("Enter End Date >");
				getEndDate.Execute();
				var endDateString = (string)getEndDate.Result;
				var endDate = Convert.ToDateTime(endDateString);
				
				var report = new Report941(beginningDate, endDate);
				report.Display();
			}
		}
	}
}
