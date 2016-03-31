using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Payroll.UI
{
    public class BillUI : MenuBase
    {
        public BillUI(int line) : base("Bills Menu", line)
        {
            base.Register(new MenuItem(MenuItemType.Option, 1, "List Bills", this.ListBills));
            base.Register(new MenuItem(MenuItemType.Option, 2, "Pay Bill", this.PayBill));
            base.Register(new MenuItem(MenuItemType.Option, 3, "Delete Bills", this.DeleteBill));            
        }

        private void ListBills()
        {
            Bill.ListAll();
        }

        private void DeleteBill()
        {
            int id;
            Bill b = new Bill();

            Console.Write("Enter a bill ID >");
            id = Int32.Parse(Console.ReadLine());
            b = Bill.Find(id);

            if (b == null)
            {
                Console.WriteLine("No bill ID: {0} found", id);
                return;
            }

            // show the bill
            Console.WriteLine("Due to: {0}", b.Payee);
            Console.WriteLine("DUE: {0} \t BY: {1}", b.Amount, b.DueDate.ToString());
            Console.WriteLine();

            Console.Write("Are you sure you want to delete? y/n >");
            ConsoleKeyInfo k = Console.ReadKey();
            if (k.Key == ConsoleKey.N)
            {
                Console.WriteLine("Aborting.");
                return;
            }
            if (k.Key == ConsoleKey.Y)
            {
                b.Delete();
                Console.WriteLine("Detleted.");
            }
            Console.WriteLine("Option not recognized. Aborting.");
            return;
        }

        private void PayBill()
        {
            int id;
            Bill b = new Bill();

            Console.Write("Enter a bill ID >");
            id = Int32.Parse(Console.ReadLine());
            b = Bill.Find(id);

            if (b == null)
            {
                Console.WriteLine("No bill ID: {0} found", id);
                return;
            }

            // show the bill
            Console.WriteLine("Due to: {0}", b.Payee);
            Console.WriteLine("DUE: {0} \t BY: {1}", b.Amount, b.DueDate.ToString());
            Console.WriteLine();

            Console.Write("How much would you like to pay? >");
            decimal amount = decimal.Parse(Console.ReadLine());

            b.Pay(amount);

            if (bool.Parse(b.Paid))
            {
                Console.WriteLine("The bill has been paid off. No balance is due.");
                b.Delete();
            }
            else
            {
                Console.WriteLine("You have paid: {0} \n"
                                + "Balance due  : {1}", amount, b.Amount);
            }

        }
    }
}
