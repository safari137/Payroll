/*
 * Created by SharpDevelop.
 * User: dilldb
 * Date: 3/18/2015
 * Time: 10:09 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections.Generic;

namespace Payroll
{
	static partial class Data
	{
		// Used for collecting all payroll data within timeframe
        public static List<Paycheck> FetchData(DateTime begin_date, DateTime end_date)
        {
        	if (begin_date.Year != end_date.Year)
        		throw new InvalidOperationException("Begin Date Year and End Date Year must be the same.");
            
        	var paychecks = new List<Paycheck>();
            var payDataFile = paycheckDataPath + begin_date.Year.ToString() + ".dat";            	         	
            	
            if (!File.Exists(payDataFile))
            	return null;
            	
            // Payroll Data
            FileStream filestream = null;                
            StreamReader reader = null;

            try
            {
                filestream = new FileStream(payDataFile, FileMode.Open, FileAccess.Read);
                reader = new StreamReader(filestream);

                // id:date:gross:fed w/h:fica w/h:medi w/h:state w/h:employer-fica:employer-medi:futa
				string data;
                while ((data = reader.ReadLine()) != null)
                {                   
                    // See if we need this date
                    var paycheckDate = Convert.ToDateTime(GetElement(data, 2));

                    if ((paycheckDate < begin_date) || (paycheckDate > end_date))
                        continue;
                    
            // id:date:gross:fed:state:soc:medi:employer-soc:employer-medi:futa:state_unemployment
                    
            		var id = Int32.Parse(GetElement(data, 1));
                    var gross = decimal.Parse(GetElement(data, 3));
                    var federalWithholding = decimal.Parse(GetElement(data, 4));
                    var stateWithholding = decimal.Parse(GetElement(data, 5));
                    var employeeSocialSecurity = decimal.Parse(GetElement(data, 6));
                    var employeeMedicare = decimal.Parse(GetElement(data, 7));
                    var employerSocialSecurity = decimal.Parse(GetElement(data, 8));
                    var employerMedicare = decimal.Parse(GetElement(data, 9));
                    var federalUnemployment = decimal.Parse(GetElement(data, 10));
                    var stateUnemployment = decimal.Parse(GetElement(data, 11));

                    var paycheck = new Paycheck(id, paycheckDate, gross, federalWithholding, stateWithholding,
                                               employeeSocialSecurity, employeeMedicare, employerSocialSecurity, 
                                               employerMedicare, federalUnemployment, stateUnemployment);
                    paychecks.Add(paycheck);
                }
            }

            finally
            {
                if (reader != null)
                    reader.Close();
                if (filestream != null)
                    filestream.Close();
            }
            	        	
            return paychecks;            	
            	
        }
	}
}
