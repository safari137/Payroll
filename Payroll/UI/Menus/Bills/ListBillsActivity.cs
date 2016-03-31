using System;
using System.Linq;
using Payroll.Domain.Bills;
using Payroll.UI.GenericActivities;
using Payroll.UI.Tools;

namespace Payroll.UI.Menus.Bills
{
	public class ListBillsActivity : Activity
	{
		public ListBillsActivity()
		{
			Title = "List Bills";
		}
		
		public override void Execute()
		{
			var bills = Bill.Bills;
				
			if (bills == null)
			    return;
			
            var billQuery = from bill in bills
                            orderby new DateTime(bill.DueDate.Year, bill.DueDate.Month, bill.DueDate.Day)
                            select bill;

            using (new ConsoleColorSchemeChanger(ConsoleColor.Black, ConsoleColor.White))
            {
			    foreach(var bill in billQuery)
			    {	
				    Console.WriteLine("Due to: {0}\t ID: {1}", bill.Vendor.ToString(), bill.Id);
				    Console.WriteLine("DUE: {0} \t BY: {1}", bill.Amount, bill.DueDate.ToString());
					
				    Console.WriteLine();
			    }
            }
		}
	}
}
