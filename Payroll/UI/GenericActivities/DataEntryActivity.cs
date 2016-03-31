using System;

namespace Payroll.UI.GenericActivities
{
    public abstract class DataEntryActivity : Activity
    {
    	private string _message;
    	
    	public DataEntryActivity()
    	{
    		this._message = "Message has not been implemented.\n"
    					  + "Please use the DataEntryActivity(string message)\n"
						  + "constructor if you do not plan to override\n"
						  + "DisplayMessage()";    			
    	}
    	
    	public DataEntryActivity(string message)
    	{
    		this._message = message;
    	}
    	
        public override void Execute()
        {
            while(true)
            {
                DisplayMessage();
                ReceiveInput();
                if (IsValid())
                    break;
            }
            ProcessInput();
        }

        protected virtual void DisplayMessage()
        {
        	Console.Write(this._message);
        }
        
        protected abstract void ReceiveInput();
        protected abstract bool IsValid();
        protected abstract void ProcessInput();
    }
}
