namespace Payroll.UI.Menus.Reports
{
	public class ReportsMenuActivity : MenuDataEntryActivity
	{
		public ReportsMenuActivity()
		{			
			Title = "Reports";
            QuitOrBackActivityTitle = "Back";
			
			RegisterMenuActivities(
				new Report940Activity(),
				new Report941Activity(),
				new ReportW2Activity(),
				new ReportStateActivity(),
				new ReportEmployeeCostActivity()
                );			
		}	
	}
}
