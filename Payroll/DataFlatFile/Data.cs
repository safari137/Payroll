using System;
using System.IO;

namespace Payroll.DataFlatFile
{

    public static partial class Data
    {
        const string DataPath = @"data/";
        const string SettingsFile = DataPath + "settings.dat";

        public static void Initialize()
        {
            if (!Directory.Exists(DataPath))
            {
            	Directory.CreateDirectory(DataPath);
            }
            try
            {
                LoadSettings();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at : " + ex.Message);
            }
        }                      
            
        private static string GetElement(string data, int element)
        {
            for (var i = 0; i< element; i++)
            {
            	// elements are separated by ':'
            	var index = data.IndexOf(':');
            		
            	// ERROR user requested an element that doesn't exist
            	if ((index < 0) && (i != (element -1)))
            		return null;
            		
            	// Check if it is the last remaining element
            	if (index < 0)
            		return data;
            		
            	// if this isn't the element we're looking for keep working through the string
            	if (i != (element -1))
            	{
            		data = data.Substring(index + 1);
            		continue;
            	}            			
            		
            	// This is our element
            	data = data.Substring(0, index);         		           		
            }
            return data;
        }                        
    } 
}
