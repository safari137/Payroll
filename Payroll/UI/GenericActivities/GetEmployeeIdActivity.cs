using System;
using Payroll.Domain;
using Payroll.UI.Tools;

namespace Payroll.UI.GenericActivities
{
    public class GetEmployeeIdActivity : DataEntryActivity
    {
        private string _input;
        private int _id;
        private Employee _employee;

        protected override void DisplayMessage()
        {
            Console.Write("Enter Employee Id >");
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

            this._employee = Employee.Find(_id);

            if (_employee == null)
                return false;

            return true;
        }

        protected override void ProcessInput()
        {
            Result = this._employee;
        }
    }
}
