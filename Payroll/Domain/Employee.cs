using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Payroll.DataContext;
using Payroll.Domain.Payroll;

namespace Payroll.Domain
{

	public class Employee
	{
        [Key]
		public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string DateOfBirth { get; set; }
        public decimal Wage { get; set; }
        public decimal Hours { get; set; }
        public decimal YearToDateIncome { get; set; }
        public int PaycycleYear { get; set; }
        public bool Married { get; set; }
        public bool IsSalary { get; set; } 
        public int FedExemptions { get; set; }
        public int StateExemptions { get; set; }

        // One to many relationship
        public virtual ICollection<Paycheck> Paychecks { get; set; }

        public static List<Employee> Employees
        {
            get
            {
                using (var payrollContext = new PayrollContext())
                {
                    return payrollContext.Employees.ToList();
                }
            }
        }
			
        public Employee()
        { }
        
		public Employee(string socialSecurityNumber, int paycycleYear, bool married, 
                        string name, string dateOfBirth, decimal wage, decimal hours,
		                decimal yearToDateIncome, bool isSalary, int fedExemptions, 
		                int stateExemptions)
		{
			this.SocialSecurityNumber = socialSecurityNumber;
			this.PaycycleYear = paycycleYear;
			this.Married = married;
			this.Name = name;
			this.DateOfBirth = dateOfBirth;
			this.Wage = wage;
			this.Hours = hours;
			this.YearToDateIncome = yearToDateIncome;
			this.IsSalary = isSalary;
			this.FedExemptions = fedExemptions;
			this.StateExemptions = stateExemptions;
		}

        // For finding Employee data by ID #
        public static Employee Find(int _id)
        {
            var foundEmployee = Employee.Employees.SingleOrDefault(e => e.Id == _id);

            return foundEmployee;
        }
		
		public string MaskSoc()
		{
			var maskMaker = SocialSecurityNumber.ToCharArray();
			var length = SocialSecurityNumber.Length;				
			var mask = "xxx-xx-";
				
			if (SocialSecurityNumber == null || length < 9)
				return null;
				
			// Get the last 4 digits of soc
			for (var i = length-4; i < length; i++)
			{
				mask += maskMaker[i];
			}
				
			return mask;					
		}
	}


}	