using System;
using Payroll.Domain;
using Payroll.UI.GenericActivities;
using Payroll.UI.Tools;

namespace Payroll.UI.Menus.Settings
{
	public class SettingsSetupActivity : Activity
	{
		public SettingsSetupActivity()
		{
			Title = "Setup";
		}
		
		public override void Execute()
		{
			using (new ConsoleColorSchemeChanger(ConsoleColor.Black, ConsoleColor.White))
			{
				var getCompanyName = new GetStringActivity("Enter Company Name >");
				getCompanyName.Execute();
				Domain.Settings.CompanyName = (string)getCompanyName.Result;
				
				var getClosingDate = new GetDateActivity("Enter Next Closing Date >");
				getClosingDate.Execute();
                Domain.Settings.CloseDate = Convert.ToDateTime((string)getClosingDate.Result);
				
				var getPayDate = new GetDateActivity("Enter Next Pay Date >");
				getPayDate.Execute();
				Domain.Settings.NextPayDate = Convert.ToDateTime((string)getPayDate.Result);
	
				this.GetPayFrequency();		
			}
		}
		
		private void GetPayFrequency()
		{
			Console.WriteLine("Payfrequency: 1 = Weekly");
			Console.WriteLine("              2 = Bi-Weekly");
			Console.WriteLine("              3 = Twice a Month");
			Console.WriteLine("              4 = Monthly");
			
			var getNumber = new GetIntegerActivity(1, 4, "Enter Payfrequency >");
			getNumber.Execute();
			var option = (int)getNumber.Result;
			
			switch (option)
			{
				case 1:
					Domain.Settings.Payfrequency = PayFrequency.Weekly;
					break;
				case 2: 
					Domain.Settings.Payfrequency = PayFrequency.BiWeekly;
					break;
				case 3:
					Domain.Settings.Payfrequency = PayFrequency.TwoWeeks;
					break;
				case 4:
					Domain.Settings.Payfrequency = PayFrequency.Monthly;
					break;
				default:
					break;
			}	
		}
	}
}
