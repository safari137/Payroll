using System;

namespace Payroll.UI.Reports
{
    public class ReportEmployeeCost : ReportBase
    {
        private decimal _employeeWages;
        private decimal _employerCost;

        public ReportEmployeeCost(DateTime beginDate, DateTime endDate)
        {
        	base.Initialize(beginDate, endDate);
        	this.Build();
        }
        
        protected override void Build()
        {
            foreach (var paycheck in base.Paychecks)
            {
                _employeeWages += paycheck.Gross;
                _employerCost += paycheck.FederalUnemployment + paycheck.StateUnemployment +
                    paycheck.EmployerSocialSecurity + paycheck.EmployerMedicare;
            }
        }

        public override void Display()
        {
            Console.WriteLine("-- Employee Cost Report: {0} - {1} --", BeginDate, EndDate);
            Console.WriteLine("Paid to Employees      : {0}", _employeeWages);
            Console.WriteLine("Employer Contributions : {0}", _employerCost);
            Console.WriteLine("TOTAL                  : {0}", (_employeeWages + _employerCost));
        }
    }
}
