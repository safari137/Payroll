using System;

namespace Payroll.UI.Reports
{
    public class StateTaxReport : ReportBase
    {
    	public StateTaxReport(DateTime beginDate, DateTime endDate)
    	{
    		base.Initialize(beginDate, endDate);
    		this.Build();
    	}

        protected override void Build()
        {
            throw new NotImplementedException();
        }

        public override void Display()
        {
            throw new NotImplementedException();
        }
    }
}
