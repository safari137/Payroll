/*
 * Created by SharpDevelop.
 * User: dilldb
 * Date: 3/3/2015
 * Time: 3:44 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace Payroll.Domain.Calculator
{
	/// <summary>
	/// Description of FederalTaxBracket.
	/// </summary>
	//2015 Tax bracket...Hard coded until Data saving features are made
	public static class FederalTaxBracket
	{
		public const decimal ONE     = .10m;
		public const decimal TWO     = .15m;
		public const decimal THREE   = .25m;
		public const decimal FOUR    = .28m;
		public const decimal FIVE    = .33m;
		public const decimal SIX     = .35m;
		public const decimal SEVEN   = .396m;
	}
}
