using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Payroll.UI2
{
    public class Menu
    {
        public readonly List<MenuItem> MenuItems { get; private set; }
        
        public string Title { get; private set; }
        public int Line { get; private set; }

        public Menu()
        {
            MenuItems = new List<MenuItem>();
        }
    }
}
