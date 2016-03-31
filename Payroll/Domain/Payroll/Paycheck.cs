using System;
using System.ComponentModel.DataAnnotations;

namespace Payroll.Domain.Payroll
{
	delegate void CalculatorEventHandler();
	
    public class Paycheck
    {
        [Key]        
    	public int PaycheckId { get; set; }
        [Required]
    	public DateTime PayDate { get; set; }
        [Required]
        public decimal Gross { get; set; } 
        [Required]
        public decimal FederalWithholding {get; set;}
        [Required]
        public decimal StateWithholding { get; set; }
        [Required]
        public decimal EmployeeSocialSecurity { get; set; }
        [Required]
        public decimal EmployeeMedicare { get; set; }
        [Required]
        public decimal EmployerSocialSecurity { get; set; }     
        [Required]
        public decimal EmployerMedicare { get; set; }
        [Required]
        public decimal FederalUnemployment { get; set; }
        [Required]
        public decimal StateUnemployment { get; set; }
        [Required]
        public bool Approved { get; set; }
        [Required]
        public int EmployeeId { get; set; }

        public virtual Employee PaidEmployee { get; set; }

        public Paycheck()
        {

        }
		
        public Paycheck(int employeeId, DateTime payDate, decimal gross, decimal federalWithholding, 
                        decimal stateWithholding, decimal employeeSocialSecurity, 
                        decimal employeeMedicare, decimal employerSocialSecurity, 
                        decimal employerMedicare, decimal federalUnemployment, 
                        decimal stateUnemployment)
        {
        	if (payDate == null)
				throw new ArgumentNullException("payDate");
        	this.EmployeeId = employeeId;
			this.PayDate = payDate;
			this.Gross = gross;
			this.FederalWithholding = federalWithholding;
			this.StateWithholding = stateWithholding;
			this.EmployeeSocialSecurity = employeeSocialSecurity;
			this.EmployeeMedicare = employeeMedicare;
			this.EmployerSocialSecurity = employerSocialSecurity;
			this.EmployerMedicare = employerMedicare;
			this.FederalUnemployment = federalUnemployment;
			this.StateUnemployment = stateUnemployment;
        }
    }
}
