using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Payroll.DataContext;

namespace Payroll.Domain.Bills
{
    [Flags]
    public enum VendorPayee
    {
        IRS = 1,
        FUTA = 2,
        STATE = 3,
        VASUI = 4
    };


    public class Bill
    {
        public VendorPayee Vendor { get; set; }

        public decimal Amount { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsPaid { get; set; }

        [Key]
        public int Id { get; set; }

        public static List<Bill> Bills 
        { 
            get 
            {
                using (var payrollContext = new PayrollContext())
                {
                    return payrollContext.Bills.ToList();
                }
            } 
        }

        public Bill()
        {

        }

        public Bill(VendorPayee payee)
        {
            this.Vendor = payee;
        }

        public Bill (VendorPayee payee, DateTime date)
        {
            this.Vendor = payee;

            this.DueDate = date;
        }

        // new Bill(vendor, amount, date, paid, id);
        public Bill(VendorPayee payee, decimal amount, DateTime date, bool isPaid)
        {
            Vendor = payee;

            this.Amount = amount;
            this.DueDate = date;
            this.IsPaid = isPaid;
        }        

        public void Add(VendorPayee vendor, decimal amount, DateTime dueDate)
        {
            this.Vendor = vendor;
            this.Amount = amount;
            this.DueDate = dueDate;

            using(var payrollContext = new PayrollContext())
            {
                payrollContext.Bills.Add(this);
                payrollContext.SaveChanges();
            }
            
        }

        // Since IRS bills for multiple pay periods are due on the same date, 
        // we will simply add the new withheld taxes to the current bill.
        public void Append(decimal amount)
        {                
            this.Amount += amount;   
        }

            
        public static Bill Find(int id)
        {
            return Bills?.FirstOrDefault(b => b.Id == id);				 
        }
			
        // Delete the Bill from Bills List
		
        public static void Delete(Bill bill)
        {
            using (var payrollContext = new PayrollContext())
            {
                payrollContext.Bills.Remove(bill);
                payrollContext.SaveChanges();
            }
        }
			
        // Pay a certain amount to the bill
        public void Pay(decimal amount)
        {
            this.Amount -= amount;

            if (Amount == 0)
            {
                IsPaid = true;                 
            }		
            return;
        }	

        /// 
        ///                  Class Helper Methods
        /// 
        /// 
        /// 
        /// 

        public static void ListAll()
        {
            if (Bills == null)
                return;
            var billQuery = from bill in Bills
                orderby bill.DueDate
                select bill;

            foreach(Bill b in billQuery)
            {	
                Console.WriteLine("Due to: {0}\t ID: {1}", b.Vendor.ToString(), b.Id);
                Console.WriteLine("DUE: {0} \t BY: {1}", b.Amount, b.DueDate.ToString());
				
                Console.WriteLine();
            }
        }
        public static Bill Find(VendorPayee payee, DateTime dueDate)
        {
            foreach (Bill b in Bills)
            {
                if (payee == b.Vendor)
                {
                    if (b.DueDate == dueDate)
                        return b;
                }
            }
            
            return null;            
        }
    }
}