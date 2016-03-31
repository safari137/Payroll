using System;

namespace Payroll.UI.GenericActivities
{
    public class GetYesOrNoActivity : DataEntryActivity
    {
        private ConsoleKeyInfo _pressedKey;
        
        public GetYesOrNoActivity(string message) : base(message)
        {
        		
        }

        protected override void ReceiveInput()
        {
            _pressedKey = Console.ReadKey();
            Console.WriteLine();
        }

        protected override bool IsValid()
        {
            var result = false;

            if (_pressedKey.Key == ConsoleKey.Y)
                result = true;
            else if (_pressedKey.Key == ConsoleKey.N)
                result = true;

            return result;
        }

        protected override void ProcessInput()
        {
            if (_pressedKey.Key == ConsoleKey.Y)
                Result = true;
            else if (_pressedKey.Key == ConsoleKey.N)
                Result = false;
        }
    }
}
