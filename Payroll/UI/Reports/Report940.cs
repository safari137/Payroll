/*
 * Created by SharpDevelop.
 * User: dilldb
 * Date: 3/4/2015
 * Time: 9:05 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using Payroll.Domain.Payroll;

namespace Payroll.UI.Reports
{
	/// <summary>
	/// Description of Report940.
	/// </summary>
	public class Report940 : ReportBase
	{
		private decimal _gross = 0;
		private decimal _futaPaid = 0;
		private decimal _futaExcess = 0;
		
		public Report940(DateTime beginDate, DateTime endDate)
		{
			base.Initialize(beginDate, endDate);
			this.Build();
		}	
	
		protected override void Build()
		{
			foreach (var paycheck in Paychecks)
			{
				_gross += paycheck.Gross;
				_futaPaid += paycheck.FederalUnemployment;
				_futaExcess += this.FutaExcessCalculator(paycheck);
			}
		}
		
		public override void Display()
		{
			Console.WriteLine("---940 Report---");
			Console.WriteLine("Beginning: {0} \t Ending: {1}", base.BeginDate, base.EndDate);
			Console.WriteLine();
			
			Console.WriteLine("Gross:                     	{0}", _gross);
			Console.WriteLine("Federal Unemployment Paid: 	{0}", _futaPaid);
			Console.WriteLine("Federal Unemployment Excess: {0}", _futaExcess);
			Console.WriteLine();
			Console.WriteLine("End Report");			
		}
		
		private decimal FutaExcessCalculator(Paycheck paycheck)
		{
			
			// We've already paid 7000, everything else is excess
			if (_gross >= 7000)
				return paycheck.Gross;
			
			// We still haven't reached 7000, there is no excess
			if ((_gross - paycheck.Gross) < 7000)
				return 0m;
			
			// We just did create some excess
			var remainder = (_gross + paycheck.Gross) - 7000;
			
			return remainder;	
		}
	}
}
