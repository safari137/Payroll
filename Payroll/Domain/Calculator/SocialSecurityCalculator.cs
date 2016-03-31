namespace Payroll.Domain.Calculator
{
    public class SocialSecurityCalculator
    {
    	private decimal _socialSecurityWithholing;
    	
        public decimal Calculate(Employee employee, decimal gross)
        {
            _socialSecurityWithholing = (Global.FICA_RATE * gross).Roundv2();

            return _socialSecurityWithholing;
        }
    }
}
