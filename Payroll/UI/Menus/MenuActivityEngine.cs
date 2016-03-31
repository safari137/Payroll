using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Payroll
{
    public class MenuActivityEngine
    {
        private HistoryManager _history = new HistoryManager();
        private Activity _currentActivity;

        public MenuActivityEngine(Activity baseActivity)
        {
            _history.ReportLast(baseActivity);
            _currentActivity = baseActivity;
        }

        public void Start()
        {            
            _currentActivity.Execute();
        }
    }
}
