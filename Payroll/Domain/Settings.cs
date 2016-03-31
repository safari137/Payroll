/*
 * Created by SharpDevelop.
 * User: mobile
 * Date: 2/9/2015
 * Time: 1:32 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;

namespace Payroll.Domain
{
    [Flags]
    public enum PayFrequency
    {
        Weekly = 1,
        BiWeekly = 2,
        TwoWeeks = 3,
        Monthly = 4
    };

    public static class Settings
    {
        public static PayFrequency Payfrequency;
        public static DateTime NextPayDate;
        public static DateTime CloseDate; 
        public static string CompanyName;
    }
}