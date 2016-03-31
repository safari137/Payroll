using System.Linq;
using Payroll.DataContext;

namespace Payroll.Domain.Bills.BillTracker
{
    public class UpdateBills
    {
        public void Update(BillTracker billtracker)
        {
            using (var context = new PayrollContext())
            {
                if (billtracker.IrsBill.Amount > 0)
                {
                    var foundIrsBill = context.Bills.SingleOrDefault(b => b.Id == billtracker.IrsBill.Id);

                    if (foundIrsBill == null)
                        context.Bills.Add(billtracker.IrsBill);
                    else
                        foundIrsBill.Amount += billtracker.IrsBill.Amount;
                }


                if (billtracker.FutaBill.Amount > 0)
                {
                    var foundFutaBill = context.Bills.SingleOrDefault(b => b.Id == billtracker.FutaBill.Id);

                    if (foundFutaBill == null)
                        context.Bills.Add(billtracker.FutaBill);
                    else
                        foundFutaBill.Amount += billtracker.FutaBill.Amount;
                }


                if (billtracker.StateBill.Amount > 0)
                {
                    var foundStateBill = context.Bills.SingleOrDefault(b => b.Id == billtracker.StateBill.Id);

                    if (foundStateBill == null)
                        context.Bills.Add(billtracker.StateBill);
                    else
                        foundStateBill.Amount += billtracker.StateBill.Amount;
                }

                if (billtracker.VaSuiBill.Amount > 0)
                {
                    var foundVaSuiBill = context.Bills.SingleOrDefault(b => b.Id == billtracker.VaSuiBill.Id);

                    if (foundVaSuiBill == null)
                        context.Bills.Add(billtracker.VaSuiBill);
                    else
                        foundVaSuiBill.Amount += billtracker.VaSuiBill.Amount;
                }

                context.SaveChanges();
            }
        }
    }
}
