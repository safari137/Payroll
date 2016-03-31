/*
 * Created by SharpDevelop.
 * User: dilldb
 * Date: 3/18/2015
 * Time: 10:10 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;

namespace Payroll
{
	static partial class Data
	{
		 ///
        /// Bills Data
        /// 

        public static void SaveBill(Bill bill)
        {
            FileStream filestream = null;                    
            StreamWriter writer = null;                    

            try
            {
                filestream = new FileStream(BillsDataFile, FileMode.Append, FileAccess.Write);
                writer = new StreamWriter(filestream);

                // vendor:amount:date:paid:id
                writer.WriteLine("{0}:{1}:{2}:{3}:{4}", bill.Vendor.ToString(), bill.Amount, bill.DueDate.Date.ToString("d"), 
                    bill.IsPaid.ToString(), bill.Id);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
                if (filestream != null)
                    filestream.Close();
            }
    }

        public static void SaveBills()
        {
            if (Bill.Bills == null)
                return;

            FileStream filestream = null;
                
            StreamWriter writer = null;
                

            try
            {
                filestream = new FileStream(BillsDataFile, FileMode.Create, FileAccess.Write);
                writer = new StreamWriter(filestream);

                foreach (Bill bill in Bill.Bills)
                {
                    // vendor:amount:date:paid:id
                    writer.WriteLine("{0}:{1}:{2}:{3}:{4}", bill.Vendor.ToString(), bill.Amount, bill.DueDate.Date.ToString("d"), 
                        bill.IsPaid.ToString(), bill.Id);
                }
            }
            finally
            {
                if (writer != null)
                    writer.Close();
                if (filestream != null)
                    filestream.Close();
            }
        }
            
        public static void LoadBills()
        {
            string data;

            if (!File.Exists(BillsDataFile))
                return;

            FileStream filestream = null;
                
            StreamReader reader = null;
                


            try
            {
                filestream = new FileStream(BillsDataFile, FileMode.Open, FileAccess.Read);
                reader = new StreamReader(filestream);

                // vendor:amount:date:paid:id
                while ((data = reader.ReadLine()) != null)
                {                        
                    VendorPayee payee = (VendorPayee)Enum.Parse(typeof(VendorPayee), GetElement(data, 1));
                    decimal amount = decimal.Parse(GetElement(data, 2));
                    var date = Convert.ToDateTime(GetElement(data, 3));
                    bool paid = bool.Parse(GetElement(data, 4));
                    int id = Int32.Parse(GetElement(data, 5));

                    Bill b = new Bill(payee, amount, date, paid, id);
                    Bill.Bills.Add(b);
                }
            }
            finally
            {
                reader.Close();
                filestream.Close();
            }
                
            return;
        }
	}
}
