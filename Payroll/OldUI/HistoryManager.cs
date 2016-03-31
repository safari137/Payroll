using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Payroll.UI
{
    public class HistoryManager : IHistoryManager
    {
        private List<MenuItem> _menuHistory = new List<MenuItem>();

        public void Pop()
        {
            _menuHistory.RemoveAt(_menuHistory.Count - 1);
        }

        public void Add(MenuItem menuItem)
        {
            _menuHistory.Insert(_menuHistory.Count, menuItem);
        }

        public MenuItem GetLast()
        {
            var count = _menuHistory.Count;

            if (count < 1)
                throw new InvalidOperationException("There is no history.");

            return _menuHistory.ElementAt(count - 1);
        }

    }
}
