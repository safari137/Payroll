namespace Payroll.Domain.Calculator
{
    public class StateCalculator
    {
    	private decimal _stateWithholding;
    	private decimal _stateExemptions;
    	private decimal _stateTaxRate;
    	
        public decimal Calculate(Employee employee, decimal gross)
        {
            _stateExemptions = employee.StateExemptions;

            // Excess is for calculating taxes above the tax bracket minimum
	 		decimal excess = 0;
	 			
	 		// Formula given by State of VA
	 		// 26 is # of pay periods
            decimal payPeriodsPerYear = 0;

            if (Settings.Payfrequency == PayFrequency.BiWeekly)
                payPeriodsPerYear = 26; 		
    	 		
            else if (Settings.Payfrequency == PayFrequency.Weekly)
                payPeriodsPerYear = 52;

            gross = (gross * payPeriodsPerYear) - (3000 + (_stateExemptions * 930));


            if (gross < 0)
            {
                _stateWithholding = 0.00m;
                return 0;
            }
	 			
            // tax bracket minimum is 17001
	 		if (gross >= 17001)
	 		{
	 			excess = gross - 17000;
	 			_stateTaxRate = .0575m;
	 			_stateWithholding += 720m;
	 		}
            // tax bracket minimum is 5001
	 		else if (gross >= 5001)
	 		{
	 			excess = gross - 5000;
	 			_stateTaxRate = .05m;
	 			_stateWithholding += 120m;
	 		}
            // tax bracket minimum is 3001
	 		else if (gross >= 3001)
	 		{
	 			excess = gross - 3000;
	 			_stateTaxRate = .03m;
	 			_stateWithholding += 60;
	 		}
	 		else 
	 		{
	 			excess = gross;
	 			_stateTaxRate = .02m;
	 		}
	 			
	 		// Calculate the tax for the year
	 		_stateWithholding += (excess * _stateTaxRate);

	 		// Divide it back into payperiods
            _stateWithholding /= payPeriodsPerYear;
               
	 		return _stateWithholding.Roundv2();
	 	}
    }
}
