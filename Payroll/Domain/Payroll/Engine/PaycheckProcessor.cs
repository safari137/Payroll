using Payroll.Domain.Calculator;

namespace Payroll.Domain.Payroll.Engine
{
    public class PaycheckProcessor
    {
        public Paycheck ProcessedPaycheck { get; private set; }

        private Employee _employee;
        private ITaxCalculator _taxcalculator;

        public PaycheckProcessor(Employee employee, ITaxCalculator taxcalculator)
        {
            this._employee = employee;
            this._taxcalculator = taxcalculator;

            this.Process();
        }

        private void Process()
        {
            decimal gross;

            // Check the tax year
            if (_employee.PaycycleYear < Settings.NextPayDate.Year)
            {
                _employee.PaycycleYear = Settings.NextPayDate.Year;
                _employee.YearToDateIncome = 0;
            }

            // Calc Gross Income
            if (_employee.IsSalary)
                gross = GrossCalculator.Calculate(_employee.Wage);
            else
                gross = GrossCalculator.Calculate(_employee.Wage, _employee.Hours);

            var paycheck = _taxcalculator.Calculate(_employee, gross);

            paycheck.EmployeeId = _employee.Id;

            ProcessedPaycheck = paycheck;
        }
    }
}
