using Payroll.UI.Menus.Bills;
using Payroll.UI.Menus.EmployeeActivity;
using Payroll.UI.Menus.Notifications;
using Payroll.UI.Menus.Reports;
using Payroll.UI.Menus.Settings;

namespace Payroll.UI.Menus.MainMenu
{
	public class MainMenuActivity : MenuDataEntryActivity
	{		
		public MainMenuActivity()
		{
            Title = "Main Menu";
            QuitOrBackActivityTitle = "Quit";

			RegisterMenuActivities(
				new EmployeeMenuActivity(),
				new ReportsMenuActivity(),
				new BillsMenuActivity(),
                new NotificationsActivity(),
                new SettingsMenuActivity()
                );
		}		
	}
}
