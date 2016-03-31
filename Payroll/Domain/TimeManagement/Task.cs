using System;

namespace Payroll.Domain.TimeManagement
{
	public class Task
	{
		public string Customer { get; set; }
		public string Job { get; set; }
		public string Description { get; set; }
		public decimal Hours { get; set; }
		public int EmployeeId { get; set; }
		public DateTime WorkDate { get; set; }
	}
}
