/*
 * Created by SharpDevelop.
 * User: dilldb
 * Date: 3/4/2015
 * Time: 9:09 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using Payroll.Reports;

namespace Payroll.UI
{
	/// <summary>
	/// Description of ReportUI.
	/// </summary>
	public class ReportUI : MenuBase
	{
        private Date _beginDate, _endDate;

        public ReportUI(int line) : base("Reports Menu", line)
        {
            base.Register(new MenuItem(MenuItemType.Option, 1, "Report 940", this.Report940));
            base.Register(new MenuItem(MenuItemType.Option, 2, "Report 941", this.Report941));
            base.Register(new MenuItem(MenuItemType.Option, 3, "Employee Cost", this.ReportEmployeeCost));
            base.Register(new MenuItem(MenuItemType.Option, 4, "Report W2", this.ReportW2));
            base.Register(new MenuItem(MenuItemType.Option, 5, "Report Job Cost", this.ReportJobCost));
        }

		private void Report940()
		{		
            this.GetDates();

            var report = new Report940(_beginDate, _endDate);
			report.Display();            
		}

        private void Report941()
        {
            this.GetDates();

            var report = new Report941(_beginDate, _endDate);
            report.Display();
        }

        private void ReportEmployeeCost()
        {
            this.GetDates();

            var report = new ReportEmployeeCost(_beginDate, _endDate);
            report.Display();
        }

        private void ReportW2()
        {
            Console.Write("Enter Year >");
            var yearInput = Console.ReadLine();
            Console.Write("Enter Id   >");
            var idInput = Console.ReadLine();

            _beginDate = new Date("1/1/" + yearInput);
            _endDate = new Date("12/31/" + yearInput);

            int id;
            Int32.TryParse(idInput, out id);

            var report = new ReportW2(_beginDate, _endDate, id);
            report.Display();

        }

        private void ReportJobCost()
        {
            Console.Write("Enter beginning date >");
            var beginDateString = Console.ReadLine();

            Console.Write("Enter ending date >");
            var endDateString = Console.ReadLine();

            Console.Write("Enter employee ID >");
            int employeeId;
            int.TryParse(Console.ReadLine(), out employeeId);

            var report = new ReportJobCost(new Date(beginDateString), new Date(endDateString),
                                          employeeId);
            var analyzedJobs = report.GetResults();


            Console.WriteLine("Employee ID: {0}", employeeId);
            foreach (var job in analyzedJobs)
            {
                Console.WriteLine("{0}:{1}\t {2}", job.JobTask.Customer, job.JobTask.Job, job.Cost);
            }
        }

        private void GetDates()
        {
            Console.Write("Enter beginning date >");
            var input = Console.ReadLine();
            _beginDate = new Date(input);

            Console.Write("Enter ending date >");
            input = Console.ReadLine();
            _endDate = new Date(input);
        }
    }
}
