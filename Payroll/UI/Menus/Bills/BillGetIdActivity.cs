using System;
using Payroll.Domain.Bills;
using Payroll.UI.GenericActivities;
using Payroll.UI.Tools;

namespace Payroll.UI.Menus.Bills
{
    public class BillGetIdActivity : DataEntryActivity
    {
        private string _input;
        private int _id;
        private Bill _bill;

        protected override void DisplayMessage()
        {
            Console.Write("Enter Bill Id >");
        }

        protected override void ReceiveInput()
        {
            _input = Console.ReadLine();
        }

        protected override bool IsValid()
        {
            var numberValidation = new NumberValidation();

            if (!numberValidation.IsValid(_input))
                return false;

            this._id = int.Parse(_input);

            this._bill = Bill.Find(_id);

            if (_bill == null)
                return false;

            return true;
        }

        protected override void ProcessInput()
        {
            Result = this._bill;
        }
    }
}
