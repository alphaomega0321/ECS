using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECS.Models;
using ECS.Services;

namespace ECS.Managers
{
    public class CheckoutManager
    {
        public Transaction CurrentTransaction { get; private set; }

        public bool ValidateEmployeeEligibility(Employee employee)
        {
            bool valid = employee.ActiveStatus;
            if (valid)
            {
                Console.WriteLine("Employee " + employee.EmployeeName + " is eligible.");
            }
            else
            {
                Console.WriteLine("Employee " + employee.EmployeeName + " is not eligible.");
            }
            return valid;
        }

        public void ProcessCheckout(Employee employee, Equipment equipment, BarcodeScanner scanner)
        {
            if (!ValidateEmployeeEligibility(employee))
            {
                Console.WriteLine("Checkout stopped. Employee is not eligible.");
                return;
            }

            string scannedCode = scanner.ScanBarcode(equipment.BarcodeValue);
            scanner.ReturnToolID(scannedCode);

            equipment.AssignToEmployee(employee);

            CurrentTransaction = new Transaction();
            CurrentTransaction.TransactionID = DatabaseManager.Instance.Transactions.Count + 1;
            CurrentTransaction.RecordCheckout();

            DatabaseManager.Instance.Transactions.Add(CurrentTransaction);
            DatabaseManager.Instance.SaveRecord("Transaction");
        }

        public void ProcessReturn(Equipment equipment)
        {
            equipment.ReturnToInventory();

            if (CurrentTransaction != null)
            {
                CurrentTransaction.RecordReturn();
                DatabaseManager.Instance.UpdateRecord("Transaction");
            }
        }
    }
}