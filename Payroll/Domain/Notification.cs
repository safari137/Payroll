using System;
using Payroll.Domain.Bills;

namespace Payroll.Domain
{
    public static class Notification
    {
    	public static DateTime GetSoonestBillDueDate()
    	{
    		var soonestBillDue = new DateTime(3000, 12, 31);
    		
    		foreach (var bill in Bill.Bills)
    		{
    			if (soonestBillDue > bill.DueDate)
    				soonestBillDue = bill.DueDate;
    		}
    		
    		return soonestBillDue;
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
