namespace Payroll.Domain.Calculator
{
    public class MedicareCalculator
    {
    	private decimal _medicareWithholding;
    	
        public decimal Calculate(Employee employee, decimal gross)
        {                
            _medicareWithholding = (Global.MEDI_RATE * gross).Roundv2();

            return _medicareWithholding;
        }
    }
}
