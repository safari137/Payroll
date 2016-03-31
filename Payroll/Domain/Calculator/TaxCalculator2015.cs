/*
 * Created by SharpDevelop.
 * User: dilldb
 * Date: 3/3/2015
 * Time: 1:54 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using Payroll.Domain.Payroll;

namespace Payroll.Domain.Calculator
{
	/// <summary>
	/// Description of TaxCalculator2015.
	/// </summary>
	public class TaxCalculator2015 : ITaxCalculator
	{
		public Paycheck Calculate(Employee employee, decimal gross)
		{
			var payDate = Settings.NextPayDate;
			var federalWithholding = new FederalCalculator().Calculate(employee, gross);
			var stateWithholding = new StateCalculator().Calculate(employee, gross);
			var employeeSocialSecurity = new SocialSecurityCalculator().Calculate(employee,gross);
			var employeeMedicare = new MedicareCalculator().Calculate(employee, gross);
			var employerSocialSecurity = employeeSocialSecurity;
			var employerMedicare = employeeMedicare;
			var federalUnemployment = new FutaCalculator().Calculate(employee, gross);
			var stateUnemployment = new StateUnemploymentCalculator().Calculate(employee, gross);
			
			var paycheck = new Paycheck(employee.Id, payDate, gross, federalWithholding, stateWithholding,
			                            employeeSocialSecurity, employeeMedicare, employerSocialSecurity, employerMedicare,
			                            federalUnemployment, stateUnemployment);
			
			return paycheck;
		}
	}
}
