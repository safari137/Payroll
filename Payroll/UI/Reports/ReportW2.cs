using System;
using System.Linq;
using Payroll.Domain;

namespace Payroll.UI.Reports
{
    public class ReportW2 : ReportBase
    {
        private int _id;
        private decimal _grossWages = 0;
        private decimal _federalWithholding = 0;
        private decimal _stateWithholding = 0, _medicareWithholding = 0, _socialSecurityWithholding = 0;
        private Employee _employee;

        public ReportW2(DateTime beginDate, DateTime endDate, int id)
        {
            this._id = id;
            base.Initialize(beginDate, endDate);
            this.Build();
        }

        protected override void Build()
        {
            if (Paychecks != null)
            {
                var employeePaychecks = Paychecks
                    .Where(p => p.EmployeeId == _id)
                    .ToList();

                foreach (var paycheck in employeePaychecks)
                {
                    this._grossWages += paycheck.Gross;
                    this._federalWithholding += paycheck.FederalWithholding;
                    this._stateWithholding += paycheck.StateWithholding;
                    this._medicareWithholding += paycheck.EmployeeMedicare;
                    this._socialSecurityWithholding += paycheck.EmployeeSocialSecurity;
                }

                _employee = Employee.Find(_id);
            }
        }

        public override void Display()
        {
            Console.WriteLine("{0} ({1}) \t W2", _employee.Name, _employee.Id);
            Console.WriteLine("Gross Wages : {0}", _grossWages);
            Console.WriteLine("Federal: -{0} \t State -{1}", _federalWithholding, _stateWithholding);
            Console.WriteLine("Medicare: -{0}\t Social Security -{0}", _medicareWithholding, _socialSecurityWithholding);
        }
    }
}
