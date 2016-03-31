using System;
using System.Collections.Generic;
using System.Linq;
using Payroll.DataFlatFile;
using Payroll.Domain;
using Payroll.Domain.TimeManagement;

namespace Payroll.UI.Reports
{
    class ReportJobCost : ReportBase
    {
        private List<Task> _tasks;
        private readonly List<Task> _jobs = new List<Task>();
        private readonly List<JobAnalysis> _analyzedJobs = new List<JobAnalysis>();

        private readonly int _employeeId;
        private decimal _totalHours = 0M;
        private decimal _grossPay;
        private bool _complete = false;

        public ReportJobCost(DateTime beginDate, DateTime endDate, int employeeId) 
        {
            this._employeeId = employeeId;
            base.Initialize(beginDate, endDate);
        }

        protected override void Build()
        {
            this.CollectTimeData();
            this.SetupJobs();
            this.GetPayData();
            this.Calculate();
            _complete = true;
        }

        public List<JobAnalysis> GetResults()
        {
            if (!_complete)
                throw new InvalidOperationException("The report has not been built!");

            return _analyzedJobs;
        }

        private void Calculate()
        {
            foreach(var job in this._jobs)
            {
                var jobAnalysis = new JobAnalysis()
                {
                    JobTask = job,
                    Cost = ((job.Hours / this._totalHours) * this._grossPay).Roundv2()
                };

                this._analyzedJobs.Add(jobAnalysis);                
            }
        }

        private void GetPayData()
        {
            var paycheck = base.Paychecks
                .FindLast(p => p.EmployeeId == _employeeId);

            if (paycheck == null)
                throw new InvalidOperationException("That user does not have any paychecks in that time period.");

            this._grossPay = paycheck.Gross;
        }

        public override void Display()
        {
            throw new NotImplementedException();
        }

        



        private void CollectTimeData()
        {
            var beginDate = new DateTime(BeginDate.Year, BeginDate.Month, BeginDate.Day);
            var endDate = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day);

            var timeManager = Data.LoadTasks(beginDate, endDate);

            var query = timeManager.GetTasks();

            _tasks = query
                .Where(t => t.EmployeeId == this._employeeId)
                .ToList();
        }

        private void SetupJobs()
        {
            foreach (var task in _tasks)
            {
                this._totalHours += task.Hours;
                AddJob(task);
            }
        }

        private void AddJob(Task task)
        {           
            var jobExists = _jobs
                .SingleOrDefault((j => j.Customer == task.Customer && j.Job == task.Job));

            if (jobExists == null)
                _jobs.Add(task);
            else
                jobExists.Hours += task.Hours;            
        }
    }

    public class JobAnalysis
    {
        public Task JobTask { get; set; }
        public decimal Cost { get; set; }

        public JobAnalysis()
        {
            JobTask = new Task();
        }
    }
}
