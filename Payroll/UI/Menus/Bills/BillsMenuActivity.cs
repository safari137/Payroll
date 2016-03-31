namespace Payroll.UI.Menus.Bills
{
	public class BillsMenuActivity : MenuDataEntryActivity
	{		
		public BillsMenuActivity()
		{
			Title = "Bills Menu";
			QuitOrBackActivityTitle = "Back";

            RegisterMenuActivities(new ListBillsActivity(), new DeleteBillActivity(), new PayBillActivity());			
		}		
		
	}
}
