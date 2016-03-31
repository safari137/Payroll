using System;
using Payroll.UI.GenericActivities;
using Payroll.UI.Reports;
using Payroll.UI.Tools;

namespace Payroll.UI.Menus.Reports
{
	public class ReportEmployeeCostActivity : Activity
	{
		public ReportEmployeeCostActivity()
		{
			Title = "Report Employee Cost";
		}
		
		public override void Execute()
		{
			using (new ConsoleColorSchemeChanger(ConsoleColor.Black, ConsoleColor.White))
            {
				var getBeginDate = new GetDateActivity("Enter Begin Date >");
				getBeginDate.Execute();
				
				var beginDateString  = (string)getBeginDate.Result;
				var beginDate = Convert.ToDateTime(beginDateString);
				
				
				var getEndDate = new GetDateActivity("Enter End Date >");
				getEndDate.Execute();
				
				var endDateString = (string)getEndDate.Result;
				var endDate = Convert.ToDateTime(endDateString);
				
				
				var report = new ReportEmployeeCost(beginDate, endDate);
				report.Display();
			}
		}
	}
}
