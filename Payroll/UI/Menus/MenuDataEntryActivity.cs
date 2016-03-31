using System;
using System.Collections.Generic;
using Payroll.UI.GenericActivities;
using Payroll.UI.Tools;

namespace Payroll.UI.Menus
{
    public abstract class MenuDataEntryActivity : DataEntryActivity
    {
        private readonly List<Activity> _activities = new List<Activity>();
		private string _input;
		private int _index;
        private QuitOrBackActivity _quitActivity = new QuitOrBackActivity("QuitOrBackActivity - Set this with base.QuitOrBackActivityTitle");

        public string QuitOrBackActivityTitle
        {
            get { return _quitActivity.Title;  }
            set { _quitActivity.Title = value; }
        }

        public MenuDataEntryActivity()
        {
            this.RegisterMenuActivities(_quitActivity);
        }

        public void RegisterMenuActivities(params Activity[] activities)
        {
            foreach (var activity in activities)
            {
                if (activity == null)
                    continue;
                _activities.Add(activity);
            }
        }

        protected override void DisplayMessage()
		{
			var index = 0;
			foreach(var activity in _activities)
			{
				Console.WriteLine("{0} : {1}", index, activity.Title);
				index++;
			}
            Console.Write(">");
		}
		
		protected override void ReceiveInput()
		{
			_input = Console.ReadLine();
		}
		
		protected override bool IsValid()
		{
			var numberValidator = new NumberValidation();
			
			if (!numberValidator.IsValid(_input))
				return false;
			
			this._index = int.Parse(_input);
			
			if (_index < 0 || _index > (_activities.Count - 1))
				return false;
			
			return true;
		}
		
		protected override void ProcessInput()
		{
			_activities[_index].Execute();
			
			if ((bool)_quitActivity.Result == false)
				this.Execute();
		}
    }
}
