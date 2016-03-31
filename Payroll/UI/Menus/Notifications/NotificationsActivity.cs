using System;
using Payroll.Domain;
using Payroll.Domain.Bills;
using Payroll.UI.GenericActivities;
using Payroll.UI.Tools;

namespace Payroll.UI.Menus.Notifications
{
	public class NotificationsActivity : Activity
	{		
		public NotificationsActivity()
		{
			Title = "Show Notifications";
		}
		
		public override void Execute()
		{
            using (new ConsoleColorSchemeChanger(ConsoleColor.Red, ConsoleColor.White))
            {
				this.ShowBillNotifications();
	            this.ShowPayrollNotifications();
            }
		}
		
		private void ShowBillNotifications()
		{
			var soonestBillDate = Notification.GetSoonestBillDueDate();
			string billMessage;
			
			if (soonestBillDate == null)
				billMessage = "There are no bills due.";
			else
			{
				billMessage = "There are " + Bill.Bills.Count + " bills due.\n";
				billMessage += "Soonest bill due on " + soonestBillDate.Date.ToString("d");	
			}
			
			Console.WriteLine(billMessage);
		}

        private void ShowPayrollNotifications()
        {
            int? daysUntilPayroll = Notification.GetPayrollDueDays();

            if (daysUntilPayroll == null)
                Console.WriteLine("Payroll due dates have not been Setup.");

            if (daysUntilPayroll == 0)
                Console.WriteLine("Payroll is due today.");

            else if (daysUntilPayroll < 0)
                Console.WriteLine("Payroll is {0} days past due.", daysUntilPayroll);

            else
                Console.WriteLine("Payroll is due in {0} days.", daysUntilPayroll);
        }
	}
}
