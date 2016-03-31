/*
 * Created by SharpDevelop.
 * User: dilldb
 * Date: 3/18/2015
 * Time: 10:05 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;

namespace Payroll
{
	static partial class Data
	{
		public static void SaveEmployee(Employee e)
        {              
            if (!Directory.Exists(DataPath))
                Directory.CreateDirectory(DataPath);

            FileStream filestream = null;
            StreamWriter writer = null;
            try
            {
                filestream = new FileStream(EmployeeDataFile, FileMode.Append, FileAccess.Write);
                writer = new StreamWriter(filestream);

                writer.WriteLine("{0}:{1}:{2}:{3}:{4}:{5}:{6}:{7}:{8}:{9}:{10}:{11}", 
                                 e.Name, e.ID, e.DateOfBirth, e.SocialSecurityNumber,
                                 e.Wage, e.Hours, e.PaycycleYear, e.YearToDateIncome, 
                                 e.Married.ToString(), e.IsSalary.ToString(), e.FedExemptions,
                                 e.StateExemptions);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
                if (filestream != null)
                    filestream.Close();
            }
        }

        private static void LoadEmployees()
        {
            bool EOF = false;       // End of File
            string data = null;

            if (!File.Exists(EmployeeDataFile))
                return;

            FileStream filestream = null;
            StreamReader reader = null;
            try
            {
                filestream = new FileStream(EmployeeDataFile, FileMode.Open, FileAccess.Read);
                reader = new StreamReader(filestream);

                while (!EOF)
                {
                    data = reader.ReadLine();
                    if (data == null)
                    {
                        EOF = true;
                    }
                    else
                        LoadEmployee(data);
                }
            }

            finally
            {
                if (reader != null)
                    reader.Close();
                if (filestream != null)
                    filestream.Close();
            }
        }

        private static void LoadEmployee(string data)
        {
            // Get the Name
            var name = GetElement(data, 1);

            // Get the ID
            var id = Int32.Parse(GetElement(data, 2));

            // Increment our global ID
            Global.ID_Manager.Employee.ReportLastKnown(id);

            // Get DOB
            var dob = GetElement(data, 3);

            // Get Soc
            var soc = GetElement(data, 4);

            //Get Hourly Rate
            var wage = decimal.Parse(GetElement(data, 5));

            // Get Hours
            var hours = decimal.Parse(GetElement(data, 6));
                
            var paycycleYear = Int32.Parse(GetElement(data, 7));
                
            var yearToDateIncome = decimal.Parse(GetElement(data, 8));
                
            var married = bool.Parse(GetElement(data, 9));
            
            var isSalary = bool.Parse(GetElement(data, 10));

            var fedExemptions = Int32.Parse(GetElement(data, 11));

            var stateExemptions = Int32.Parse(GetElement(data, 12));

            var employee = new Employee(soc, paycycleYear, married, id, name, dob, wage, 
                                        hours, yearToDateIncome, isSalary, fedExemptions,
                                        stateExemptions);
            Employee.Add(employee);

        }

        public static void SaveEmployeeFile()
        {
            const string tempPath = EmployeeDataFile + ".temp";

            if (File.Exists(tempPath))
                File.Delete(tempPath);
            try
            {
                File.Copy(EmployeeDataFile, tempPath);
                File.Delete(EmployeeDataFile);
            }
            catch (Exception ex)
            {
            	Console.WriteLine("ERROR : {0}", ex.Message);
            }              
                
            foreach(Employee e in Employee.Employees)
            {
                SaveEmployee(e);
            }
        }                      
	}
}
