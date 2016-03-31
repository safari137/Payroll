using System;
using System.Linq;
using Payroll.DataContext;
using Payroll.Domain.Bills;
using Payroll.UI.GenericActivities;
using Payroll.UI.Tools;

namespace Payroll.UI.Menus.Bills
{
	public class DeleteBillActivity : Activity
	{
		public DeleteBillActivity()
		{
			Title = "Delete Bill";
		}
		
		public override void Execute()
		{
			using (new ConsoleColorSchemeChanger(ConsoleColor.Black, ConsoleColor.White))
            {
				var getBillId = new BillGetIdActivity();
				getBillId.Execute();
				var bill = (Bill)getBillId.Result;
				
				string message = "Delete (" + bill.Vendor.ToString() +
					":" + bill.DueDate + ":" + bill.Amount + ") Y/N? >";
				var deleteConfirmation = new GetYesOrNoActivity(message);
				deleteConfirmation.Execute();
				
				if ((bool)deleteConfirmation.Result == true)
                {
                    using (var context = new PayrollContext())
                    {
                        var billFromDatabase = context.Bills.SingleOrDefault(b => b.Id == bill.Id);

                        if (billFromDatabase != null)
                        {
                            context.Bills.Remove(billFromDatabase);
                            context.SaveChanges();
                        }
                    }
                }
			}
		}
	}
}
