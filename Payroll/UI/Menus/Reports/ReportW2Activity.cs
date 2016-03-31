using System;
using Payroll.Domain;
using Payroll.UI.GenericActivities;
using Payroll.UI.Reports;
using Payroll.UI.Tools;

namespace Payroll.UI.Menus.Reports
{

	public class ReportW2Activity : Activity
	{
		public ReportW2Activity()
		{
			Title = "Report W2";
		}
		
		public override void Execute()
		{
			using (new ConsoleColorSchemeChanger(ConsoleColor.Black, ConsoleColor.White))
            {
				var getEmployeeById = new GetEmployeeIdActivity();
				getEmployeeById.Execute();
				var employee = (Employee)getEmployeeById.Result;
				
				var getBeginDate = new GetDateActivity("Enter Start Date >");
				getBeginDate.Execute();
				var beginDateString = (string)getBeginDate.Result;
				var beginDate = Convert.ToDateTime(beginDateString);
				
				var getEndDate = new GetDateActivity("Enter End Date >");
				getEndDate.Execute();
				var endDateString = (string)getEndDate.Result;
				var endDate = Convert.ToDateTime(endDateString);
				
				var report = new ReportW2(beginDate, endDate, employee.Id);
				report.Display();
			}
		}
	}
}
