using Payroll.UI.GenericActivities;

namespace Payroll.UI.Menus
{
	public class QuitOrBackActivity : Activity
	{
		public QuitOrBackActivity(string title)
		{
			Title = title;
			Result = false;
		}
		
		public override void Execute()
		{
			Result = true;
		}
	}
}
