using System;
using System.Collections.Generic;
using System.IO;
using Payroll.Domain.TimeManagement;

namespace Payroll.DataFlatFile
{
	partial class Data
	{
	    private const string TaskFile = DataPath + "tasks.dat";

		public static void SaveTasks(List<Task> tasks)
		{
			FileStream filestream = null;
			StreamWriter writer = null;
			
			if (tasks == null)
				return;
			if (tasks.Count < 0)
				return;

			try
			{
				filestream = new FileStream(TaskFile, FileMode.OpenOrCreate, FileAccess.Write);
				writer = new StreamWriter(filestream);
				
				foreach (var task in tasks)
				{
					writer.WriteLine("{0}:{1}:{2}:{3}:{4}:{5}", task.EmployeeId, task.Customer,
				                	task.Job, task.Description, task.Hours, task.WorkDate.ToString("M/d/yyyy"));
				}
			}
			
			finally
			{
				writer.Close();
				filestream.Close();
			}
		}
		public static void SaveTask(Task task)
		{
			FileStream filestream = null;
			StreamWriter writer = null;

			try
			{
				filestream = new FileStream(TaskFile, FileMode.Append, FileAccess.Write);
				writer = new StreamWriter(filestream);
				
				writer.WriteLine("{0}:{1}:{2}:{3}:{4}:{5}", task.EmployeeId, task.Customer,
                                task.Job, task.Description, task.Hours, task.WorkDate.ToString("M/d/yyyy"));
			}
			
			finally
			{
			    writer?.Close();
			    filestream?.Close();
			}
		}
		public static TimeManager LoadTasks(DateTime beginDate, DateTime endDate)
		{
			FileStream filestream = null;
			StreamReader reader = null;
			
			var timeManager = new TimeManager()
			{
				BeginDate = beginDate,
				EndDate = endDate
			};
			
			try
			{
				filestream = new FileStream(TaskFile, FileMode.Open, FileAccess.Read);
				reader = new StreamReader(filestream);
				
				string data;
				while ((data = reader.ReadLine()) != null)
				{
					var date = Convert.ToDateTime(Data.GetElement(data, 6));
					
					if (date < beginDate || date > endDate)
						continue;
					
					var task = ExtractTask(data);
					task.WorkDate = date;
					timeManager.AddTask(task);
				}
			}
			finally
			{
			    reader?.Close();
			    filestream?.Close();
			}

		    return timeManager;
		}
		
		private static Task ExtractTask(string data)
		{
			int employeeId;
			int.TryParse(Data.GetElement(data, 1), out employeeId);
			
			var customer = Data.GetElement(data, 2);
			var job = Data.GetElement(data, 3);
			var description = Data.GetElement(data, 4);
			
			decimal hours;
			
			decimal.TryParse(Data.GetElement(data, 5), out hours);
			
			var task = new Task()
			{
				EmployeeId = employeeId,
				Customer = customer,
				Job = job,
				Description = description,
				Hours = hours
			};
			
			return task;
		}
	}
}
