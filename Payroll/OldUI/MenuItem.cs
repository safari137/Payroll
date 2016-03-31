using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Payroll.UI
{
    [Flags]
    public enum MenuItemType
    {
        Menu = 10,
        Option = 20
    };

    public class MenuItem
    {
        private readonly List<MenuItem> _menuItems;

        public MenuItemType Type;

        public List<MenuItem> MenuItems
        {
            get
            {
                if (_menuItems == null)
                    throw new InvalidOperationException("_menuItems is null");

                return _menuItems;
            }
        }
        public int Line { get; private set; }
        public string Title { get; private set; }
        public Action Run { get; private set; }

        public MenuItem(MenuItemType type, int line, string name, Action run)
        {
            this.Type = type;   
            this.Line = line;
            this.Title = name;
            this.Run = run;

            if ((run == null && this.Type == MenuItemType.Option) && line != 0)
                throw new InvalidOperationException("Run cannot be null for a MenuItemType option.");

            // Add a default back method for new menu's
            if (this.Type == MenuItemType.Menu && run == null)
            {
                this._menuItems = new List<MenuItem>();

                var exitMenu = new MenuItem(MenuItemType.Option, 0, "Exit", null);
                this.MenuItems.Add(exitMenu);
            }
        }

        public void RegisterMenu(MenuItem menuItem)
        {
            if (menuItem == null)
                throw new InvalidOperationException("Cannot register null MenuItem.");

            if (menuItem.Type == MenuItemType.Option)
            {
                if (menuItem.Line == 0)
                    this._menuItems.RemoveAt(0);
            }

            var existsAlreadyTest = this._menuItems.SingleOrDefault(m => m.Line == menuItem.Line);
            if (existsAlreadyTest != null)            
                throw new InvalidOperationException("MenuItem with line " + menuItem.Line + " already exists");
            
            if (_menuItems.Count != menuItem.Line)
                throw new InvalidOperationException("You cannot skip a line number when adding menu items.");
            
            this._menuItems.Insert(menuItem.Line, menuItem);
        }
    }
}
