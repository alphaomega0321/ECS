using System;
using System.Collections.Generic;
using System.Linq;
using ECS.Models;
using ECS.Managers;
using ECS.Services;

namespace ECS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DatabaseManager.Instance.OpenConnection();

            // Seed sample data
            Employee employee = new Employee
            {
                EmployeeID = 1,
                EmployeeName = "John Smith",
                SkillClassification = "Electrician",
                Department = "Maintenance",
                ActiveStatus = true
            };

            Equipment equipment = new Equipment
            {
                EquipmentID = 1001,
                EquipmentName = "Power Drill",
                EquipmentType = "Tool",
                Status = "Available",
                BarcodeValue = "DRILL-1001",
                Location = "Depot Shelf A"
            };

            DatabaseManager.Instance.Employees.Add(employee);
            DatabaseManager.Instance.EquipmentItems.Add(equipment);

            // Managers
            CheckoutManager checkoutManager = new CheckoutManager();
            InventoryManager inventoryManager = new InventoryManager();
            ReservationManager reservationManager = new ReservationManager();
            ReportManager reportManager = new ReportManager();
            AuditLog auditLog = new AuditLog();
            BarcodeScanner scanner = new BarcodeScanner();

            bool running = true;

            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("===== Equipment Checkout System =====");
                Console.WriteLine("1. Checkout Equipment");
                Console.WriteLine("2. Return Equipment");
                Console.WriteLine("3. Make Reservation");
                Console.WriteLine("4. Generate Report");
                Console.WriteLine("5. View Audit Log");
                Console.WriteLine("6. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("\n--- Checkout Equipment ---");
                        checkoutManager.ProcessCheckout(employee, equipment, scanner);
                        inventoryManager.UpdateAvailability(equipment, "Checked Out");
                        auditLog.RecordAction("Equipment checked out.");
                        break;

                    case "2":
                        Console.WriteLine("\n--- Return Equipment ---");
                        checkoutManager.ProcessReturn(equipment);
                        inventoryManager.UpdateAvailability(equipment, "Available");
                        auditLog.RecordAction("Equipment returned.");
                        break;

                    case "3":
                        Console.WriteLine("\n--- Make Reservation ---");
                        Reservation reservation = reservationManager.SubmitReservation(DateTime.Now.AddDays(1));
                        reservationManager.ApproveReservation(reservation);
                        auditLog.RecordAction("Reservation created and approved.");
                        break;

                    case "4":
                        Console.WriteLine("\n--- Generate Report ---");
                        Console.WriteLine("1. Usage Report");
                        Console.WriteLine("2. Loss Report");
                        Console.Write("Select report type: ");
                        string reportChoice = Console.ReadLine();

                        if (reportChoice == "1")
                            reportManager.GenerateReport("usage");
                        else if (reportChoice == "2")
                            reportManager.GenerateReport("loss");
                        else
                            Console.WriteLine("Invalid report selection.");

                        break;

                    case "5":
                        Console.WriteLine("\n--- Audit Log ---");
                        auditLog.ViewLog();
                        break;

                    case "6":
                        running = false;
                        Console.WriteLine("Exiting system...");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }
    }
}