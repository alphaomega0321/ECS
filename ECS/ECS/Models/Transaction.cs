using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public int EmployeeID { get; set; }
        public int EquipmentID { get; set; }
        public DateTime CheckoutDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string TransactionStatus { get; set; }

        public Transaction()
        {
            TransactionStatus = "Open";
        }

        public void RecordCheckout()
        {
            CheckoutDate = DateTime.Now;
            TransactionStatus = "Checked Out";
            Console.WriteLine("Checkout recorded.");
        }

        public void RecordReturn()
        {
            ReturnDate = DateTime.Now;
            TransactionStatus = "Returned";
            Console.WriteLine("Return recorded.");
        }

        public void UpdateTransaction(string status)
        {
            TransactionStatus = status;
            Console.WriteLine("Transaction updated to " + status + ".");
        }
    }
}