using System;
using System.Globalization;

namespace Payroll.UI.Tools
{
    public class DateValidation : Validation
    {

        public override bool IsValid(string input)
        {
            string[] formats = {
                                   "M/d/yy",
                                   "M/dd/yy",
                                   "M/d/yyyy",
                                   "M/dd/yyyy",
                                   "MM/dd.yy",
                                   "MM/dd/yyyy"
                               };

            DateTime parsedDateTime;

            if (DateTime.TryParseExact(input, formats, new CultureInfo("en-US"), DateTimeStyles.None, out parsedDateTime))
                return true;

            return false;
        }
    }
}
