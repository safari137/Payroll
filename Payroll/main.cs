using System;
using Payroll.DataFlatFile;
using Payroll.UI.Menus.MainMenu;
using Payroll.UI.Menus.Notifications;
using Payroll.UI.Menus.Settings;

namespace Payroll
{
    public class Program
    {
        static void Main()
        {
        	Console.ForegroundColor = ConsoleColor.White;
            Data.Initialize();            
            
            new DisplaySettingsActivity().Execute();
            new NotificationsActivity().Execute();

           	var mainMenuActivity = new MainMenuActivity();
           	mainMenuActivity.Execute();
        }
    }
}
