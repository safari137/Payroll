using System;
using System.Collections.Generic;
using System.Linq;
using Payroll.DataFlatFile;

namespace Payroll.Domain.TimeManagement
{
	public class TimeManager
	{
		public DateTime BeginDate { get; set; }
		public DateTime EndDate { get; set; }
		
		private List<Task> _tasks = new List<Task>();		
	
	
		public void Load()
		{
			if (this.BeginDate == null || this.EndDate == null)
			{
				throw new InvalidOperationException("BeginDate and EndDate have not been initialized!");
			}
			
			var tempManager = Data.LoadTasks(this.BeginDate, this.EndDate);
			
			this._tasks = tempManager.GetTasks();
		}
		
		public void AddTask(Task task)
		{
			if (task == null)
				throw new InvalidOperationException("Task cannot be null.");

			this._tasks.Add(task);
		}
		
		public List<Task> GetTasks(int employeeId)
		{
			var query = this._tasks
				.Where(t => t.EmployeeId == employeeId)
				.ToList();
			
			return query;
		}
		
		public List<Task> GetTasks()
		{
			return _tasks;
		}
		
		public void ModifyTask(int employeeId, DateTime date, string job, Task newTask)
		{
			var task = this.LocateTask(employeeId, date, job);
			
			task = newTask;
		}
		
		public void RemoveTask(int employeeId, DateTime date, string job)
		{
			var task = this.LocateTask(employeeId, date, job);
			
			this._tasks.Remove(task);
		}
		
		private Task LocateTask(int employeeId, DateTime date, string job)
		{
			var task = this._tasks
				.SingleOrDefault(t => t.EmployeeId == employeeId && t.WorkDate == date
				                 && t.Job == job);
			
			return task;
		}
	}
}
