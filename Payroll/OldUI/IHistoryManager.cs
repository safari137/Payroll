using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Payroll.UI
{
    public interface IHistoryManager
    {
        void Pop();
        void Add(MenuItem menuItem);
        MenuItem GetLast();
    }
}
