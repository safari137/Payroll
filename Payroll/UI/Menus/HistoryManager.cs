using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Payroll
{
    public class HistoryManager
    {
        private Stack<Activity> _history = new Stack<Activity>();

        public void ReportLast(Activity activity)
        {
            _history.Push(activity);
        }

        public Activity GoBack()
        {
            if (_history.Count > 1)
                _history.Pop();

            return _history.Peek();
        }
    }
}
