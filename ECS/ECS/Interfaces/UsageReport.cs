using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECS.Interfaces;
using ECS.Models;
using ECS.Services;

namespace ECS.Reports
{
    public class UsageReport : IReport
    {
        public void Generate()
        {
            Console.WriteLine("Generating equipment usage report...");

            if (DatabaseManager.Instance.Transactions.Count == 0)
            {
                Console.WriteLine("No transaction records found.");
                return;
            }

            foreach (Transaction transaction in DatabaseManager.Instance.Transactions)
            {
                Console.WriteLine(
                    "Transaction ID: " + transaction.TransactionID +
                    " | Employee ID: " + transaction.EmployeeID +
                    " | Equipment ID: " + transaction.EquipmentID +
                    " | Status: " + transaction.TransactionStatus
                );
            }
        }
    }
}