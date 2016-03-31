using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Payroll.UI
{
    public class NotificationsUI : MenuBase
    {
        public NotificationsUI(int line) : base("Notifications Menu", line)
        {
            base.Register(new MenuItem(MenuItemType.Option, 1, "Show Notifications", this.ShowNotifications));
        }

        private void ShowNotifications()
        {
            new NotificationsActivity().Execute();
        }
    }
}
