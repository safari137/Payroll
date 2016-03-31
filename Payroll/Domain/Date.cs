 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Payroll
{
    public class Date
    {
        public string month;
        public string day;
        public string year;

        public int Month
        {
            get
            {
                return Int32.Parse(month);
            }
            set
            {
                month = value.ToString();
            }
        }

        public int Day
        {
            get
            {
                return Int32.Parse(day);
            }
            set
            {
                day = value.ToString();
            }
        }

        public int Year
        {
            get
            {
                return Int32.Parse(year);
            }
            set
            {
                year = value.ToString();
            }
        }
        public Date(string date)
        {
            int index;

            // month
            index = date.IndexOf('/');
            month = date.Substring(0, index);
            date = date.Substring(index + 1);

            // day
            index = date.IndexOf('/');
            day = date.Substring(0, index);
            date = date.Substring(index + 1);

            // year
            year = date;
        }

        public Date (int _month, int _day, int _year)
        {
            month = _month.ToString();
            day = _day.ToString();
            year = _year.ToString();
        }

        public Date(int _year, int quarter, bool begin)
        {
            switch (quarter)
            {
                case 1:
                    this.month = "1";
                    break;
                case 2:
                    this.month = "4";
                    break;
                case 3:
                    this.month = "7";
                    break;
                case 4:
                    this.month = "10";
                    break;
                default:
                    this.month = "1";
                    break;
            }

            if (begin)
            {
                this.day = "1";
            }
            else
            {
                this.month = (Int32.Parse(this.month) + 2).ToString();
                this.day = DateTime.DaysInMonth(_year, Month).ToString();
            }
            this.year = _year.ToString();
        }
            
        public override string ToString()
        {
            return string.Format("{0}/{1}/{2}", month, day, year);
        }
			
		public static bool operator > (Date d1, Date d2)
		{
            if (d1.Year > d2.Year)
                return true;
            else if (d1.Year < d2.Year)
                return false;

            if (d1.Month > d2.Month)
                return true;
            else if (d1.Month < d2.Month)
                return false;

            if (d1.Day > d2.Day)
                return true;

            return false;				
		}			
			

            
			
		public static bool operator < (Date d1, Date d2)
		{
			if (d1.Year < d2.Year)
				return true;
			else if(d1.Year > d2.Year)
				return false;

            if (d1.Month < d2.Month)
                return true;
            else if (d1.Month > d2.Month)
                return false;
				
			if (d1.Day < d2.Day)
				return true;
				
			return false;
		}
			
		public static Date operator + (Date d1, int days)
		{
			DateTime datemath = new DateTime(d1.Year, d1.Month, d1.Day);
			datemath = datemath.AddDays(days);
				
			d1.Month = datemath.Month;
			d1.Day = datemath.Day;
			d1.Year = datemath.Year;
				
			return d1;
		}

        /// 
        ///
        ///                     Helper Methods
        ///
        ///

        public static int DayDifference(Date date1, Date date2)
        {
            DateTime datetime1 = new DateTime(date1.Year, date1.Month, date1.Day);
            DateTime datetime2 = new DateTime(date2.Year, date2.Month, date2.Day);

            var timespan = datetime1 - datetime2;

            int DayDifference = timespan.Days;

            return DayDifference;
        }
    }
}
