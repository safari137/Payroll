using Payroll.Domain.Payroll;

namespace Payroll.Domain.Calculator
{
    public interface ITaxCalculator
    {
        Paycheck Calculate(Employee employee, decimal gross);
    }
}
