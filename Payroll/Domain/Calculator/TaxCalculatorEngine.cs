using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Payroll
{
    public class TaxCalculatorEngine
    {
        private IList<ITaxCalculator> _taxCalculators = new List<ITaxCalculator>();

        public TaxCalculatorEngine(params ITaxCalculator[] taxCalculators)
        {
            this.Add(taxCalculators);
        }

        public void Run(Employee employee, decimal gross)
        {
            foreach (var taxCalculator in _taxCalculators)
                taxCalculator.Calculate(employee, gross);

            _taxCalculators.Clear();
        }

        public void Add(params ITaxCalculator[] taxCalculators)
        {
            if (taxCalculators == null)
                throw new InvalidOperationException("Null tax calculator not allowed.");

            foreach (var taxCalculator in taxCalculators)
            {
                if (taxCalculator == null)
                    throw new InvalidOperationException("Null tax calculator not allowed.");

                _taxCalculators.Add(taxCalculator);
            }
        }
    }
}
