using Payroll.UI.Menus.EmployeeActivity.AddEmployee;
using Payroll.UI.Menus.EmployeeActivity.DeleteEmployee;
using Payroll.UI.Menus.EmployeeActivity.ListEmployees;
using Payroll.UI.Menus.EmployeeActivity.ModifyEmployee;
using Payroll.UI.Menus.EmployeeActivity.PayEmployees;
using Payroll.UI.Menus.EmployeeActivity.SetHours;

namespace Payroll.UI.Menus.EmployeeActivity
{
    public class EmployeeMenuActivity : MenuDataEntryActivity
    {
        public EmployeeMenuActivity()
        {
        	Title = "Employee Menu";
            QuitOrBackActivityTitle = "Back";
        	
            RegisterMenuActivities( new AddEmployeeActivity(), new DeleteEmployeeActivity(), new ListEmployeesActivity(),
                new SetHoursActivity(), new ModifyEmployeeActivity(),new PayEmployeesActivity(), new ApproveChecksActivity());
        }
    }
}
