using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Payroll.UI
{
    public abstract class MenuBase
    {
        private MenuItem _menu;

        public MenuBase(string name, int line)
        {
            _menu = new MenuItem(MenuItemType.Menu, line, name, null);
        }

        protected virtual void Register(params MenuItem[] menuItems)
        {
            foreach (var menuItem in menuItems)
            {
                if (_menu.MenuItems.Count != menuItem.Line)
                    throw new InvalidOperationException("You cannot skip a line number when adding menu items.");

                _menu.RegisterMenu(menuItem);
            }
        }

        // Deliver the Menu to the UI Loader
        public virtual MenuItem Deliver()
        {
            return _menu;
        }
        
    }
}
