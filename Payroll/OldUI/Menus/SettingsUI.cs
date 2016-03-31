using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Payroll.UI
{
    class SettingsUI : MenuBase
    {
        public SettingsUI(int line) : base("Settings Menu", line)
        {
            base.Register(new MenuItem(MenuItemType.Option, 1, "Setup", this.Setup));
        }

        private void Setup()
        {
            Console.Write("Company Name >");
            Settings.CompanyName = Console.ReadLine();

            Console.Write("Next Pay Date >");
            Settings.NextPayDate = new Date(Console.ReadLine());

            Console.Write("Next Close Date >");
            Settings.CloseDate = new Date(Console.ReadLine());


            Console.WriteLine("1= weekly, 2= biweekly, 3=every two weeks, 4=monthly");
            Console.Write("Pay Frequency >");

            switch (Console.ReadLine())
            {
                case "1":
                    Settings.Payfrequency = PayFrequency.Weekly;
                    break;
                case "2":
                    Settings.Payfrequency = PayFrequency.BiWeekly;
                    break;
                case "3":
                    Settings.Payfrequency = PayFrequency.TwoWeeks;
                    break;
                case "4":
                    Settings.Payfrequency = PayFrequency.Monthly;
                    break;
                default:
                    Settings.Payfrequency = PayFrequency.BiWeekly;
                    break;
            }

            Data.SaveSettings();
        }
    }
}
