using System;
using Payroll.Domain;
using Payroll.Domain.Payroll;

namespace Payroll.UI.Menus.EmployeeActivity.PayEmployees
{
    public class DisplayPaycheckConsole : IDisplay
    {
        public void Display(Employee employee, Paycheck paycheck)
        {
            Console.WriteLine("NAME: {0} ({1})", employee.Name, employee.Id);
            Console.WriteLine("Gross Pay:\t {0} \t YTD : {1}", paycheck.Gross, employee.YearToDateIncome);
            Console.WriteLine("Net Pay:\t {0}", paycheck.Gross - paycheck.FederalWithholding
                                                - paycheck.EmployeeMedicare - paycheck.EmployeeSocialSecurity
                                                - paycheck.StateWithholding);
            Console.WriteLine("State\t: -{0}\tFederal\t: -{1}", paycheck.StateWithholding,
                                                                paycheck.FederalWithholding);
            Console.WriteLine("FICA\t: -{0}\tMediC\t: -{1}", paycheck.EmployeeSocialSecurity,
                                                                paycheck.EmployeeMedicare);
            Console.WriteLine("-");
            Console.WriteLine("-----Employer-----");
            Console.WriteLine("FICA: -{0} \t Medicare           : -{1}", paycheck.EmployerSocialSecurity, paycheck.EmployerMedicare);
            Console.WriteLine("FUTA: -{0} \t State Unemployment : -{1}", paycheck.FederalUnemployment, paycheck.StateUnemployment);
            Console.WriteLine("\n");
        }
    }
}
