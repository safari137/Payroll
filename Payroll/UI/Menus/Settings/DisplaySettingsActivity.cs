using System;
using Payroll.UI.GenericActivities;
using Payroll.UI.Tools;

namespace Payroll.UI.Menus.Settings
{
	public class DisplaySettingsActivity : Activity
	{
		public DisplaySettingsActivity()
		{
			Title = "Display Settings";
		}
		
		public override void Execute()
		{
			using (new ConsoleColorSchemeChanger(ConsoleColor.Blue, ConsoleColor.White))
            {
            	Console.WriteLine("Company: {0} \n"            	                  
                            + "Next Pay Date: {1} \n"
                            + "Closing Date: {2} \n"
                            + "Frequency: {3} \n", Domain.Settings.CompanyName, Domain.Settings.NextPayDate.Date.ToString("d"),
                                        Domain.Settings.CloseDate.Date.ToString("d"), Domain.Settings.Payfrequency.ToString());

            }
		}
	}
}
