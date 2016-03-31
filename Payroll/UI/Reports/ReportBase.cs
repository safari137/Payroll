using System;
using System.Collections.Generic;
using System.Linq;
using Payroll.DataContext;
using Payroll.Domain.Payroll;

namespace Payroll.UI.Reports
{
	/// <summary>
	/// Description of Report.
	/// </summary>
	public abstract class ReportBase
	{
        private List<Paycheck> _paychecks = new List<Paycheck>();
        private bool _hasBeenInitialized;

        protected DateTime BeginDate { get; private set; }
        protected DateTime EndDate { get; private set; }
        protected List<Paycheck> Paychecks 
        { 
            get
            {
                if (_hasBeenInitialized)
                    return _paychecks;
                else
                    throw new InvalidOperationException("You must initialize the report before building or distributing.");
            }
            private set
            {
                _paychecks = value;
            }
        } 

		
		// Build the desired data from the list of Paychecks
		protected abstract void Build();
		// Show the data
		public abstract void Display();
		
		protected virtual void Initialize(DateTime beginDate, DateTime endDate)
		{
            this.BeginDate = beginDate;
            this.EndDate = endDate;

            using (var context = new PayrollContext())
            {
                Paychecks = context.Paychecks
                    .Where(p => p.PayDate >= beginDate && p.PayDate <= endDate)
                    .Where(p => p.Approved == true)
                    .ToList();
            }

            _hasBeenInitialized = true;
		}
	}
}
