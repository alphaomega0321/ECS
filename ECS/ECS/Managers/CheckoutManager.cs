using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECS.Models;
using ECS.Services;
using ECS.Tests;

namespace ECS.Managers
{
    public class CheckoutManager
    {
        // Stores the most recent transaction processed by the checkout manager.
        public Transaction CurrentTransaction { get; private set; }

        /// <summary>
        /// Validates whether an employee is allowed to check out a specific equipment item.
        /// Checks employee status and skill classification requirements.
        /// </summary>
        public bool ValidateEmployeeEligibility(Employee employee, Equipment equipment)
        {
            // Block checkout if the employee is inactive.
            if (!employee.ActiveStatus)
            {
                Console.WriteLine("Employee " + employee.EmployeeName + " is not active.");
                return false;
            }

            // Block checkout if the equipment requires a skill the employee does not have.
            if (!string.IsNullOrWhiteSpace(equipment.RequiredSkill) &&
                !employee.SkillClassification.Equals(equipment.RequiredSkill, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Employee skill classification does not match equipment requirement.");
                return false;
            }

            Console.WriteLine("Employee " + employee.EmployeeName + " is eligible.");
            return true;
        }

        /// <summary>
        /// Processes an equipment checkout transaction.
        /// This method validates equipment availability, employee eligibility,
        /// barcode identification, transaction creation, and database storage.
        /// </summary>
        public bool ProcessCheckout(Employee employee, Equipment equipment, BarcodeScanner scanner)
        {
            // Prevent checkout if equipment is already checked out, damaged, reserved, or unavailable.
            if (equipment.Status != "Available")
            {
                Console.WriteLine("Checkout stopped. Equipment is currently not available.");
                return false;
            }

            // Confirm that the selected employee is eligible for the selected equipment.
            if (!ValidateEmployeeEligibility(employee, equipment))
            {
                Console.WriteLine("Checkout stopped. Employee is not eligible.");
                return false;
            }

            // Simulate barcode scanning by reading the equipment barcode value.
            string scannedCode = scanner.ScanBarcode(equipment.BarcodeValue);
            scanner.ReturnToolID(scannedCode);

            // Assign the equipment to the employee and update equipment state.
            equipment.AssignToEmployee(employee);

            // Create a transaction record for the checkout.
            CurrentTransaction = new Transaction();
            CurrentTransaction.TransactionID = DatabaseManager.Instance.Transactions.Count + 1;
            CurrentTransaction.EmployeeID = employee.EmployeeID;
            CurrentTransaction.EquipmentID = equipment.EquipmentID;
            CurrentTransaction.RecordCheckout();

            // Save transaction to the simulated database.
            DatabaseManager.Instance.Transactions.Add(CurrentTransaction);
            DatabaseManager.Instance.SaveRecord("Transaction");

            return true;
        }

        /// <summary>
        /// Processes an equipment return.
        /// A return is only allowed if the equipment is currently checked out
        /// and an open checkout transaction exists.
        /// </summary>
        public bool ProcessReturn(Equipment equipment)
        {
            // Locate the most recent open transaction for this equipment item.
            Transaction openTransaction = DatabaseManager.Instance.Transactions
                .LastOrDefault(t => t.EquipmentID == equipment.EquipmentID &&
                                    t.TransactionStatus == "Checked Out");

            // Prevent return if the item was not checked out or no open transaction exists.
            if (equipment.Status != "Checked Out" || openTransaction == null)
            {
                Console.WriteLine("Return stopped. Equipment is not currently checked out.");
                return false;
            }

            // Return equipment to inventory and close the transaction.
            equipment.ReturnToInventory();
            openTransaction.RecordReturn();

            // Update transaction data in the simulated database.
            DatabaseManager.Instance.UpdateRecord("Transaction");

            return true;
        }
    }
}