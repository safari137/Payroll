/*
 * Created by SharpDevelop.
 * User: dilldb
 * Date: 3/18/2015
 * Time: 10:07 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;

namespace Payroll
{
	static partial class Data
	{
		public static void SavePayCheck(Paycheck paycheck)
        {
        	string year = paycheck.PayDate.Year.ToString();
        	
            string paycheckFile = paycheckDataPath + year + ".dat";
                

            if(!Directory.Exists(paycheckDataPath))              
                Directory.CreateDirectory(paycheckDataPath);

            FileStream filestream = null;
            StreamWriter writer = null;

            try
            {
                filestream = new FileStream(paycheckFile, FileMode.Append, FileAccess.Write);
                writer = new StreamWriter(filestream);

            // id:date:gross:fed:state:soc:medi:employer-soc:employer-medi:futa:state_unemployment
            writer.WriteLine("{0}:{1}:{2}:{3}:{4}:{5}:{6}:{7}:{8}:{9}:{10}", paycheck.EmployeeId, paycheck.PayDate.Date.ToString("d"),
                                                        paycheck.Gross, paycheck.FederalWithholding,
                                                        paycheck.StateWithholding, paycheck.EmployeeSocialSecurity,
                                                        paycheck.EmployeeMedicare, paycheck.EmployerSocialSecurity,
                                                        paycheck.EmployerMedicare, paycheck.FederalUnemployment, 
                                                        paycheck.StateUnemployment);
            }
            finally
            {
                if (filestream != null)
                    writer.Close();
                if (writer != null)
                    filestream.Close();
            }
        }
	}
}
