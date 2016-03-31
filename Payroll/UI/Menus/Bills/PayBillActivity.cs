using System;
using System.Linq;
using Payroll.DataContext;
using Payroll.Domain.Bills;
using Payroll.UI.GenericActivities;
using Payroll.UI.Tools;

namespace Payroll.UI.Menus.Bills
{
	public class PayBillActivity : Activity
	{
		public PayBillActivity()
		{
			Title = "Pay Bill";
		}
		
		public override void Execute()
		{
			using (new ConsoleColorSchemeChanger(ConsoleColor.Black, ConsoleColor.White))
            {
				var getBillId = new BillGetIdActivity();
				getBillId.Execute();
				var bill = (Bill)getBillId.Result;
				
				Console.WriteLine("Bill: {0}\t Due: {1}", bill.Vendor.ToString(), bill.DueDate);
				Console.WriteLine("Amount: {0}", bill.Amount);
				
				var getAmountActivity = new GetDecimalActivity("Enter Amount to Pay >");
				getAmountActivity.Execute();
				var amount = (decimal)getAmountActivity.Result;
							
				bill.Pay(amount);

                using (var context = new PayrollContext())
                {
                    var billFromDatabase = context.Bills.SingleOrDefault(b => b.Id == bill.Id);

                    if (bill.IsPaid)
                    {
                        Console.WriteLine("The bill has been paid off. No balance is due.");                        

                        if (billFromDatabase != null)
                            context.Bills.Remove(billFromDatabase);
                    }
                    else
                    {
                        billFromDatabase.Amount = bill.Amount;
                        Console.WriteLine("You have paid: {0} \n"
                                        + "Balance due  : {1}", amount, bill.Amount);
                    }

                    context.SaveChanges();
                }
			}
		}
	}
}
