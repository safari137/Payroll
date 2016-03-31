using System;
using Payroll.Domain.Payroll;

namespace Payroll.Domain.Bills.BillTracker
{
	public class BillTracker
	{
        public Bill IrsBill { get; private set; }
        public Bill FutaBill { get; private set; }
        public Bill StateBill { get; private set; }
        public Bill VaSuiBill { get; private set; }
        
        private readonly DateTime _federalDueDate;
        private readonly DateTime _stateDueDate;
        private readonly DateTime _futaDueDate;
        private readonly DateTime _vaSuiDueDate;
        
        public BillTracker()
		{
        	int year = Settings.NextPayDate.Year;
            int month = Settings.NextPayDate.Month + 1;
            int federalDay = 15;
            int stateDay = 25;
            
            this._federalDueDate = new DateTime(year, month, federalDay);
            this._stateDueDate = new DateTime(year , month, stateDay);
            this._futaDueDate = new DateTime(year + 1, 1, 31);
            this._vaSuiDueDate = this.GetVaSuiDueDate(Settings.NextPayDate);

            if ((this.FutaBill = Bill.Find(VendorPayee.FUTA, this._futaDueDate)) == null)
            {	
            	this.FutaBill = new Bill(VendorPayee.FUTA, this._futaDueDate);
            }
            if ((this.IrsBill = Bill.Find(VendorPayee.IRS, this._federalDueDate)) == null)
            {
            	this.IrsBill = new Bill(VendorPayee.IRS, this._federalDueDate);
            }
            if ((this.StateBill = Bill.Find(VendorPayee.STATE, this._stateDueDate)) == null)
            {
            	this.StateBill = new Bill(VendorPayee.STATE, _stateDueDate);
            }
            if ((this.VaSuiBill = Bill.Find(VendorPayee.VASUI, this._vaSuiDueDate)) == null)
            {
            	this.VaSuiBill = new Bill(VendorPayee.VASUI, _vaSuiDueDate);
            }
		}              

        
		public void AppendBill(Paycheck paycheck)
        {   
            decimal tax = 0m;

            // IRS Bill
            tax = paycheck.FederalWithholding + (paycheck.EmployeeMedicare + paycheck.EmployerMedicare) 
                    + (paycheck.EmployeeSocialSecurity + paycheck.EmployerSocialSecurity);
            IrsBill.Append(tax);

            // FUTA Bill
            tax = paycheck.FederalUnemployment;
            FutaBill.Append(tax);

            // STATE Bill
            tax = paycheck.StateWithholding;
	        StateBill.Append(tax);

            // VA SUI Bill
            tax = paycheck.StateUnemployment;
            VaSuiBill.Append(tax);

            var billUpdater = new UpdateBills();
            billUpdater.Update(this);
        }

        private DateTime GetVaSuiDueDate(DateTime date)
        {
            var month = date.Month;
            var year = date.Year;
           
            if (month < 1 || month > 12)
                throw new InvalidOperationException("Month is out of range");

            DateTime dueDate = date;

            if (month > 0 && month <= 3)
            {
                dueDate = new DateTime(year, 4, DateTime.DaysInMonth(year, 4));
            }
            else if (month > 3 && month <= 6)
                dueDate = new DateTime(year, 7, DateTime.DaysInMonth(year, 7));
            else if (month > 6 && month <= 9)
                dueDate = new DateTime(year, 10, DateTime.DaysInMonth(year, 10));
            else if (month > 9 && month <= 12)
                dueDate = new DateTime(year + 1, 1, DateTime.DaysInMonth(year + 1, 1));

            return dueDate;
        }

	}
}
