
using System;
using System.Collections.Generic;
using Payroll.TimeManagement;
using System.Linq;

namespace Payroll.UI
{
	public class TimeManagerUI : MenuBase
	{
		public TimeManagerUI(int line) : base("Time Manager", line)
		{
			base.Register(new MenuItem(MenuItemType.Option, 1, "Add Time", this.AddTime));
			base.Register(new MenuItem(MenuItemType.Option, 2, "Modify Time", this.ModifyTime));
			base.Register(new MenuItem(MenuItemType.Option, 3, "Remove Time", this.RemoveTime));
			base.Register(new MenuItem(MenuItemType.Option, 4, "List Time", this.ListTime));
		}
		
		private void AddTime()
		{
			Console.Write("Employee Id >");
			int employeeId;
			int.TryParse(Console.ReadLine(), out employeeId);
			
			Console.Write("Customer >");
			var customer = Console.ReadLine();
			
			Console.Write("Job >");
			var job = Console.ReadLine();
			
			Console.Write("Description >");
			var description = Console.ReadLine();
			
			Console.Write("Date (Format: month/day/year)>");
			dynamic date = Console.ReadLine();
			date = Convert.ToDateTime(date);
			
			Console.Write("Hours >");
			decimal hours;
			decimal.TryParse(Console.ReadLine(), out hours);
			
			var task = new Task()
			{
				EmployeeId = employeeId,
				Customer = customer,
				Job = job,
				Description = description,
				WorkDate = date,
				Hours = hours
			};
			
			Data.SaveTask(task);
		}
		
		private void ModifyTime()
		{			
		
		}
		
		private void RemoveTime()
		{
			
		}
		
		private void ListTime()
		{
			Console.Write("Begin Date >");
			var beginDate = Console.ReadLine();
			var beginDateTime = Convert.ToDateTime(beginDate);
			
			Console.Write("End Date >");
			var endDate = Console.ReadLine();
			var endDateTime = Convert.ToDateTime(endDate);
			
			var timeManager = Data.LoadTasks(beginDateTime, endDateTime);
			
			var timetasks = timeManager.GetTasks()
				.OrderBy(t => t.EmployeeId)
				.ThenBy(t => t.WorkDate)
				.ThenBy(t => t.Hours)
                .ToList();
				
			
			foreach(var timetask in timetasks)
			{
				Console.WriteLine("ID   : {0}\tCustomer: {1}", timetask.EmployeeId, timetask.Customer);
				Console.WriteLine("Job  : {0}\tDescription: {1}", timetask.Job, timetask.Description);
				Console.WriteLine("Date : {0}\tHours: {1}", timetask.WorkDate.ToString("MM/dd/yy"), timetask.Hours);

                Console.WriteLine();
			}
		}
	}
}
