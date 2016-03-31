using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Payroll.UI
{
    public class UserInterfaceLoader
    {
        private MenuEngine _payrollMenuEngine;

        public UserInterfaceLoader(MenuItem baseMenu)
        {
            _payrollMenuEngine = new MenuEngine(baseMenu);            
        }

        public MenuEngine MakeEngine()
        {
            var employeeMenu = new EmployeeUI(1).Deliver();
            var reportsMenu = new ReportUI(2).Deliver();           
            var billMenu = new BillUI(3).Deliver();
            var notificationMenu = new NotificationsUI(4).Deliver();
            var settingsMenu = new SettingsUI(5).Deliver();

            _payrollMenuEngine.RegisterMenu(reportsMenu, employeeMenu, settingsMenu, billMenu, notificationMenu);

            return this._payrollMenuEngine;
        }
    }
}
