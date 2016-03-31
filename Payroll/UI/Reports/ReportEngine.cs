/*
 * Created by SharpDevelop.
 * User: dilldb
 * Date: 3/4/2015
 * Time: 8:39 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections.Generic;

namespace Payroll.UI.Reports
{
	/// <summary>
	/// Description of ReportEngine.
	/// </summary>
	public class ReportEngine
	{
		private List<ReportBase> _reports = new List<ReportBase>();
		
		
		public void AddReport(params ReportBase[] reports)
		{
			if (reports == null)
				throw new InvalidOperationException("Report cannot be null.");
			
			foreach (var report in reports)
				_reports.Add(report);
		}
		
		public void Start()
		{
			if (_reports.Count < 1)
				throw new InvalidOperationException("No reports have been added");
			
			foreach (var report in _reports)
				report.Display();			
		}		
	}
}
