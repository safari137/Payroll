using System;
using System.Collections.Generic;

namespace Payroll.UI.Reports
{
    public class Report941 : ReportBase
    {
        private readonly List<int> _paidEmployeeIdList = new List<int>();
        private int _paidEmployees = 0;
        private decimal _totalFederalTax;
        private decimal _totalWages;
        private decimal _socialSecurity;
        private decimal _medicare;
        private decimal _federalWithholding;

        public Report941(DateTime beginDate, DateTime endDate)
        {
        	base.Initialize(beginDate, endDate);
        	this.Build();
        }
                
        protected override void Build()
        {
            foreach(var paycheck in base.Paychecks)
            {
                if (!HasBeenChecked(paycheck.EmployeeId))
                    this._paidEmployees++;

                this._socialSecurity += paycheck.EmployeeSocialSecurity + paycheck.EmployerSocialSecurity;
                this._medicare += paycheck.EmployeeMedicare + paycheck.EmployerMedicare;
                this._federalWithholding += paycheck.FederalWithholding;              

                this._totalWages += paycheck.Gross;
            }

            this._totalFederalTax += this._socialSecurity + this._medicare + this._federalWithholding;
        }

        public override void Display()
        {
            Console.WriteLine("------Report 941  ({0} - {1})------", BeginDate, EndDate);
            Console.WriteLine();
            Console.WriteLine("Total Employees Paid-(Line 1 )-: {0}", this._paidEmployees);
            Console.WriteLine("Total Wages----------(Line 2 )-: {0}", this._totalWages);
            Console.WriteLine("Total Payments-------(Line 11)-: {0}", this._totalFederalTax);
            Console.WriteLine("Social Security-------------------{0}", this._socialSecurity);
            Console.WriteLine("Medicare--------------------------{0}", this._medicare);
            Console.WriteLine("Federal Withholding---------------{0}", this._federalWithholding);
        }

        private bool HasBeenChecked(int id)
        {
            foreach (int _id in _paidEmployeeIdList)
            {
                if (id == _id)
                    return true;                
            }

            _paidEmployeeIdList.Add(id);
            
            return false;
        }  
    }
}
