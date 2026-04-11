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

        public bool ValidateEmployeeEligibility(Employee employee, Equipment equipment)
        {
            if (!employee.ActiveStatus)
            {
                Console.WriteLine("Employee " + employee.EmployeeName + " is not active.");
                return false;
            }

            if (!string.IsNullOrWhiteSpace(equipment.RequiredSkill) &&
                !employee.SkillClassification.Equals(equipment.RequiredSkill, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Employee skill classification does not match equipment requirement.");
                return false;
            }

            Console.WriteLine("Employee " + employee.EmployeeName + " is eligible.");
            return true;
        }

        public bool ProcessCheckout(Employee employee, Equipment equipment, BarcodeScanner scanner)
        {
            if (equipment.Status != "Available")
            {
                Console.WriteLine("Checkout stopped. Equipment is currently not available.");
                return false;
            }

            if (!ValidateEmployeeEligibility(employee, equipment))
            {
                Console.WriteLine("Checkout stopped. Employee is not eligible.");
                return false;
            }

            string scannedCode = scanner.ScanBarcode(equipment.BarcodeValue);
            scanner.ReturnToolID(scannedCode);

            equipment.AssignToEmployee(employee);

            CurrentTransaction = new Transaction();
            CurrentTransaction.TransactionID = DatabaseManager.Instance.Transactions.Count + 1;
            CurrentTransaction.EmployeeID = employee.EmployeeID;
            CurrentTransaction.EquipmentID = equipment.EquipmentID;
            CurrentTransaction.RecordCheckout();

            DatabaseManager.Instance.Transactions.Add(CurrentTransaction);
            DatabaseManager.Instance.SaveRecord("Transaction");

            return true;
        }

        public bool ProcessReturn(Equipment equipment)
        {
            Transaction openTransaction = DatabaseManager.Instance.Transactions
                .LastOrDefault(t => t.EquipmentID == equipment.EquipmentID &&
                                    t.TransactionStatus == "Checked Out");

            if (equipment.Status != "Checked Out" || openTransaction == null)
            {
                Console.WriteLine("Return stopped. Equipment is not currently checked out.");
                return false;
            }

            equipment.ReturnToInventory();
            openTransaction.RecordReturn();
            DatabaseManager.Instance.UpdateRecord("Transaction");

            return true;
        }
    }
}