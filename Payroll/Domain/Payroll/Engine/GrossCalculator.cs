namespace Payroll.Domain.Payroll.Engine
{
    public static class GrossCalculator
    {
        // Salary version
        public static decimal Calculate(decimal salary)
        {
            var gross = 0m;

            if (Settings.Payfrequency == PayFrequency.Monthly)
            {
                gross = (salary / 12).Roundv2();
            }
            else if (Settings.Payfrequency == PayFrequency.TwoWeeks)
            {
                gross = (salary / 24).Roundv2();
            }
            else if (Settings.Payfrequency == PayFrequency.BiWeekly)
            {
                gross = (salary / 26m).Roundv2();
            }
            else if (Settings.Payfrequency == PayFrequency.Weekly)
            {
                gross = (salary / 52m).Roundv2();
            }

            return gross;
        }

        // Hourly Version
        public static decimal Calculate(decimal rate, decimal hours)
        {
            decimal overtimeHours = 0;
            decimal regularHours = 0;
            decimal regularPay = 0;
            decimal overtimePay = 0;

            decimal grossPay = 0;

            if (Settings.Payfrequency == PayFrequency.BiWeekly)
            {
                if (hours > 80)
                {
                    overtimeHours = hours - 80;
                    regularHours = 80;
                }
                else
                    regularHours = hours;
            }


            else if (Settings.Payfrequency == PayFrequency.Weekly)
            {
                if (hours > 40)
                {
                    overtimeHours = hours - 40;
                    regularHours = 40;
                }
                else
                    regularHours = hours;
            }


            if (overtimeHours > 0m)
            {
                overtimePay += (rate * 1.5m) * overtimeHours;
                overtimePay = overtimePay.Roundv2();
            }

            regularPay = (regularHours * rate).Roundv2();
            grossPay = regularPay + overtimePay;

            return grossPay;
        }
    }
}
