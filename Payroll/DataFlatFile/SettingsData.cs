/*
 * Created by SharpDevelop.
 * User: dilldb
 * Date: 3/18/2015
 * Time: 10:12 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.IO;
using Payroll.Domain;

namespace Payroll.DataFlatFile
{
	static partial class Data
	{
	    public static void SaveSettings()
	    {
            FileStream filestream = null;
            StreamWriter writer = null;

            try
            {
                filestream = new FileStream(SettingsFile, FileMode.Create, FileAccess.Write);
                writer = new StreamWriter(filestream);

                writer.WriteLine("{0}:{1}:{2}:{3}", Settings.CompanyName, Settings.NextPayDate.Date.ToString("d"), 
                                                Settings.CloseDate.Date.ToString("d"), Settings.Payfrequency);
            }
            finally
            {
                writer?.Close();
                filestream?.Close();
            }
	        	
	    }
	        
	    public static void LoadSettings()
	    {
	        if (!File.Exists(SettingsFile))
	        	return;

            FileStream filestream = null;
            StreamReader reader = null;

            try
            {
                filestream = new FileStream(SettingsFile, FileMode.Open, FileAccess.Read);
                reader = new StreamReader(filestream);

                var data = reader.ReadLine();

                Settings.CompanyName = GetElement(data, 1);
                Settings.NextPayDate = Convert.ToDateTime(GetElement(data, 2));
                Settings.CloseDate = Convert.ToDateTime(GetElement(data, 3));
                Settings.Payfrequency = (PayFrequency)Enum.Parse(typeof(PayFrequency), GetElement(data, 4));
            }
            finally
            {
                reader.Close();
                filestream.Close();
            }					
	    }	        
	}
}
