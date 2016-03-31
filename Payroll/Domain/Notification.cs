using System;
using System.Linq;
using Payroll.Domain.Bills;

namespace Payroll.Domain
{
    public static class Notification
    {
    	public static DateTime GetSoonestBillDueDate()
    	{
    	    var soonestBillDue = Bill.Bills
    	        .OrderByDescending(b => b.DueDate)
    	        .FirstOrDefault();

    	    return soonestBillDue != null ? soonestBillDue.DueDate : new DateTime(1900, 1, 1);
    	}

        public static int? GetPayrollDueDays()
        {
            if (Settings.CloseDate == null)
                return null;

            var today = DateTime.Today;                   
               
            var dayDifference = Settings.CloseDate - today;                               

            return dayDifference.Days;
        }
    }
}
