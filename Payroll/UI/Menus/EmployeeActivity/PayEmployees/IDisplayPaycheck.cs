using Payroll.Domain;
using Payroll.Domain.Payroll;

namespace Payroll.UI.Menus.EmployeeActivity.PayEmployees
{
    public interface IDisplay
    {
        void Display(Employee employee, Paycheck paycheck);
    }
}
