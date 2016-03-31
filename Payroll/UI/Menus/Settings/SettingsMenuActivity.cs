namespace Payroll.UI.Menus.Settings
{
	public class SettingsMenuActivity : MenuDataEntryActivity
	{		
		public SettingsMenuActivity()
		{
			Title = "Settings";
            QuitOrBackActivityTitle = "Back";
			
			RegisterMenuActivities(
				new DisplaySettingsActivity(),
				new SettingsSetupActivity()
                );
		}	
	}
}
