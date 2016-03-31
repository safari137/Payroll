using System;

namespace Payroll.UI.GenericActivities
{
    public abstract class Activity
    {
    	private object _result;

        public object Result
        {
            get
            {
                if (_result == null)
                    throw new InvalidOperationException("The Activity result is null.");

                return _result;
            }
            protected set { _result = value; }
        }

        public string Title { get; set; }
        public abstract void Execute();
    }
}
