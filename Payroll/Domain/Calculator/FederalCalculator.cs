namespace Payroll.Domain.Calculator
{
    public class FederalCalculator
    {
		private const decimal _exemptionValue = 153.80m;
		private decimal _fedTaxRate;
		private decimal _federalWithholding;
		
        public decimal Calculate(Employee employee, decimal gross)
        {            
            // Excess is for calculating taxes above the tax bracket minimum
            decimal excess = 0m;
            // IRS allows us to remove exemptions from the gross income before calculating
            gross = gross - (employee.FedExemptions * _exemptionValue);

            // Assign Tax Bracket	
            if (!employee.Married)
            {
                if (gross > 15981)
                {
                    _fedTaxRate = FederalTaxBracket.SEVEN;
                    _federalWithholding += 4615.38m;
                    excess = gross - 15981m;
                }
                else if (gross > 15915)
                {
                    _fedTaxRate = FederalTaxBracket.SIX;
                    _federalWithholding += 4592.28m;
                    excess = gross - 15915m;
                }
                else if (gross > 7369)
                {
                    _fedTaxRate = FederalTaxBracket.FIVE;
                    _federalWithholding += 1772.10m;
                    excess = gross - 7369m;
                }
                else if (gross > 3579)
                {
                    _fedTaxRate = FederalTaxBracket.FOUR;
                    _federalWithholding += 710.90m;
                    excess = gross - 3579m;
                }
                else if (gross > 1529)
                {
                    _fedTaxRate = FederalTaxBracket.THREE;
                    _federalWithholding += 198.40m;
                    excess = gross - 1529m;
                }
                else if (gross > 443)
                {
                    _fedTaxRate = FederalTaxBracket.TWO;
                    _federalWithholding += 35.50m;
                    excess = gross - 443m;
                }
                else if (gross > 88)
                {
                    _fedTaxRate = FederalTaxBracket.ONE;
                    _federalWithholding = 0m;
                    excess = gross - 88m;
                }
                else if (gross < 88)
                {
                    _fedTaxRate = 0;
                }
            }

            else
            {
                if (gross > 18210)
                {
                    _fedTaxRate = FederalTaxBracket.SEVEN;
                    _federalWithholding += 4999.96m;
                    excess = gross - 18210m;
                }
                else if (gross > 16158)
                {
                    _fedTaxRate = FederalTaxBracket.SIX;
                    _federalWithholding += 4281.76m;
                    excess = gross - 16158m;
                }
                else if (gross > 9194)
                {
                    _fedTaxRate = FederalTaxBracket.FIVE;
                    _federalWithholding += 1983.64m;
                    excess = gross - 9194m;
                }
                else if (gross > 6146)
                {
                    _fedTaxRate = FederalTaxBracket.FOUR;
                    _federalWithholding += 1130.20m;
                    excess = gross - 6146m;
                }
                else if (gross > 3212)
                {
                    _fedTaxRate = FederalTaxBracket.THREE;
                    _federalWithholding += 396.70m;
                    excess = gross - 3212m;
                }
                else if (gross > 1040)
                {
                    _fedTaxRate = FederalTaxBracket.TWO;
                    _federalWithholding += 70.90m;
                    excess = gross - 1040m;
                }
                else if (gross > 331)
                {
                    _fedTaxRate = FederalTaxBracket.ONE;
                    _federalWithholding += 0;
                    excess = gross - 331m;
                }
                else if (gross < 331)
                {
                    _fedTaxRate = 0;
                }
            }

            _federalWithholding += (_fedTaxRate * excess).Roundv2();
            
            return _federalWithholding;
        }
    }
}
