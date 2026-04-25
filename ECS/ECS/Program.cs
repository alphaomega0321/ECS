using System;
using System.Collections.Generic;
using System.Linq;
using ECS.Models;
using ECS.Managers;
using ECS.Services;
using ECS.Security;

namespace ECS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Initialize system and simulate database connection
            DatabaseManager.Instance.OpenConnection();

            // Load initial seed data for testing and demonstration
            SeedData();

            // Instantiate all core system components
            AuthenticationService authService = new AuthenticationService();
            CheckoutManager checkoutManager = new CheckoutManager();
            InventoryManager inventoryManager = new InventoryManager();
            ReservationManager reservationManager = new ReservationManager();
            ReportManager reportManager = new ReportManager();
            AuditLog auditLog = new AuditLog();
            BarcodeScanner scanner = new BarcodeScanner();

            bool running = true;

            // Main application loop
            while (running)
            {
                // Ensure user is authenticated before accessing system features
                if (!SessionManager.IsLoggedIn)
                {
                    RunLogin(authService);
                }

                Console.WriteLine();

                // System header (visual branding for UI)
                Console.WriteLine("████████╗ ██████╗  ██████╗ ██╗      ██████╗██████╗ ██╗██████╗ ");
                Console.WriteLine("╚══██╔══╝██╔═══██╗██╔═══██╗██║     ██╔════╝██╔══██╗██║██╔══██╗");
                Console.WriteLine("   ██║   ██║   ██║██║   ██║██║     ██║     ██████╔╝██║██████╔╝");
                Console.WriteLine("   ██║   ██║   ██║██║   ██║██║     ██║     ██╔══██╗██║██╔══██╗");
                Console.WriteLine("   ██║   ╚██████╔╝╚██████╔╝███████╗╚██████╗██║  ██║██║██████╔╝");
                Console.WriteLine("   ╚═╝    ╚═════╝  ╚═════╝ ╚══════╝ ╚═════╝╚═╝  ╚═╝╚═╝╚═════╝ ");

                // Display logged-in user and role (important for role-based access)
                Console.WriteLine("Logged in as: " + SessionManager.CurrentUser.Username + " (" + SessionManager.CurrentUser.Role + ")");

                // Main system menu
                Console.WriteLine("1. Checkout Equipment");
                Console.WriteLine("2. Return Equipment");
                Console.WriteLine("3. Make Reservation");
                Console.WriteLine("4. Generate Report");
                Console.WriteLine("5. View Audit Log");
                Console.WriteLine("6. View Equipment Inventory");
                Console.WriteLine("7. Logout");
                Console.WriteLine("8. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                // Process user menu selection
                switch (choice)
                {
                    case "1":
                        HandleCheckout(checkoutManager, inventoryManager, auditLog, scanner);
                        break;

                    case "2":
                        HandleReturn(checkoutManager, inventoryManager, auditLog);
                        break;

                    case "3":
                        HandleReservation(reservationManager, auditLog);
                        break;

                    case "4":
                        HandleReport(reportManager, auditLog);
                        break;

                    case "5":
                        Console.WriteLine();
                        Console.WriteLine("--- Audit Log ---");
                        auditLog.ViewLog();
                        break;

                    case "6":
                        // Displays all equipment and their current status
                        inventoryManager.DisplayAllEquipment();
                        break;

                    case "7":
                        // Ends current user session
                        authService.Logout();
                        break;

                    case "8":
                        // Terminates the application
                        running = false;
                        Console.WriteLine("Exiting system...");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }

        /// <summary>
        /// Handles login process and loops until valid credentials are provided.
        /// </summary>
        private static void RunLogin(AuthenticationService authService)
        {
            while (!SessionManager.IsLoggedIn)
            {
                Console.WriteLine();
                Console.WriteLine("===== ECS Login =====");

                Console.Write("Username: ");
                string username = Console.ReadLine();

                Console.Write("Password: ");
                string password = ReadPassword();

                // Attempt to authenticate user
                UserAccount user = authService.Login(username, password);

                if (user == null)
                {
                    Console.WriteLine("Please try again.");
                }
            }
        }

        /// <summary>
        /// Reads password input and masks characters for security.
        /// </summary>
        private static string ReadPassword()
        {
            string password = string.Empty;
            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey(true);

                // Add character and display mask
                if (keyInfo.Key != ConsoleKey.Backspace && keyInfo.Key != ConsoleKey.Enter)
                {
                    password += keyInfo.KeyChar;
                    Console.Write("*");
                }
                // Handle backspace functionality
                else if (keyInfo.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
            }
            while (keyInfo.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password;
        }

        /// <summary>
        /// Seeds initial data into the simulated database.
        /// Prevents duplicate data if already loaded.
        /// </summary>
        private static void SeedData()
        {
            if (DatabaseManager.Instance.Employees.Count > 0)
            {
                return;
            }

            // Employees
            DatabaseManager.Instance.Employees.Add(new Employee
            {
                EmployeeID = 1,
                EmployeeName = "John Smith",
                SkillClassification = "Electrician",
                Department = "Maintenance",
                ActiveStatus = true
            });

            DatabaseManager.Instance.Employees.Add(new Employee
            {
                EmployeeID = 2,
                EmployeeName = "Maria Lopez",
                SkillClassification = "Welder",
                Department = "Maintenance",
                ActiveStatus = true
            });

            DatabaseManager.Instance.Employees.Add(new Employee
            {
                EmployeeID = 3,
                EmployeeName = "Chris Taylor",
                SkillClassification = "Carpenter",
                Department = "Maintenance",
                ActiveStatus = false
            });

            // Equipment
            DatabaseManager.Instance.EquipmentItems.Add(new Equipment
            {
                EquipmentID = 1001,
                EquipmentName = "Power Drill",
                EquipmentType = "Tool",
                RequiredSkill = "Electrician",
                Status = "Available",
                BarcodeValue = "DRILL-1001",
                Location = "Depot Shelf A"
            });

            DatabaseManager.Instance.EquipmentItems.Add(new Equipment
            {
                EquipmentID = 1002,
                EquipmentName = "Welding Torch",
                EquipmentType = "Tool",
                RequiredSkill = "Welder",
                Status = "Available",
                BarcodeValue = "TORCH-1002",
                Location = "Depot Shelf B"
            });

            DatabaseManager.Instance.EquipmentItems.Add(new Equipment
            {
                EquipmentID = 1003,
                EquipmentName = "Circular Saw",
                EquipmentType = "Tool",
                RequiredSkill = "Carpenter",
                Status = "Available",
                BarcodeValue = "SAW-1003",
                Location = "Depot Shelf C"
            });

            DatabaseManager.Instance.EquipmentItems.Add(new Equipment
            {
                EquipmentID = 1004,
                EquipmentName = "Extension Ladder",
                EquipmentType = "Tool",
                RequiredSkill = "",
                Status = "Available",
                BarcodeValue = "LADDER-1004",
                Location = "Depot Shelf D"
            });

            // User accounts (authentication + roles)
            DatabaseManager.Instance.UserAccounts.Add(new UserAccount
            {
                UserID = 1,
                EmployeeID = 1,
                Username = "jsmith",
                PasswordHash = PasswordHelper.HashPassword("Password123"),
                Role = "DepotStaff"
            });

            DatabaseManager.Instance.UserAccounts.Add(new UserAccount
            {
                UserID = 2,
                EmployeeID = 2,
                Username = "mlopez",
                PasswordHash = PasswordHelper.HashPassword("Welder123"),
                Role = "Supervisor"
            });

            DatabaseManager.Instance.UserAccounts.Add(new UserAccount
            {
                UserID = 3,
                EmployeeID = 3,
                Username = "ctaylor",
                PasswordHash = PasswordHelper.HashPassword("Carpenter123"),
                Role = "Admin"
            });
        }

        /// <summary>
        /// Handles equipment checkout process including validation and logging.
        /// </summary>
        private static void HandleCheckout(
            CheckoutManager checkoutManager,
            InventoryManager inventoryManager,
            AuditLog auditLog,
            BarcodeScanner scanner)
        {
            Console.WriteLine();
            Console.WriteLine("--- Checkout Equipment ---");

            Employee employee = SelectEmployee();
            if (employee == null)
            {
                Console.WriteLine("Employee not found.");
                return;
            }

            inventoryManager.DisplayAllEquipment();

            Equipment equipment = SelectEquipment();
            if (equipment == null)
            {
                Console.WriteLine("Equipment not found.");
                return;
            }

            // Perform checkout validation and processing
            bool success = checkoutManager.ProcessCheckout(employee, equipment, scanner);

            if (success)
            {
                inventoryManager.UpdateAvailability(equipment, "Checked Out");

                // Log action for audit tracking
                auditLog.RecordAction(
                    "User " + SessionManager.CurrentUser.Username +
                    " checked out equipment. Employee ID: " + employee.EmployeeID +
                    ", Equipment ID: " + equipment.EquipmentID
                );
            }
        }

        /// <summary>
        /// Handles equipment return process and updates system records.
        /// </summary>
        private static void HandleReturn(
            CheckoutManager checkoutManager,
            InventoryManager inventoryManager,
            AuditLog auditLog)
        {
            Console.WriteLine();
            Console.WriteLine("--- Return Equipment ---");

            inventoryManager.DisplayAllEquipment();

            Equipment equipment = SelectEquipment();
            if (equipment == null)
            {
                Console.WriteLine("Equipment not found.");
                return;
            }

            bool success = checkoutManager.ProcessReturn(equipment);

            if (success)
            {
                inventoryManager.UpdateAvailability(equipment, "Available");

                // Log return action
                auditLog.RecordAction(
                    "User " + SessionManager.CurrentUser.Username +
                    " returned equipment. Equipment ID: " + equipment.EquipmentID
                );
            }
        }

        /// <summary>
        /// Handles reservation creation and approval.
        /// </summary>
        private static void HandleReservation(
            ReservationManager reservationManager,
            AuditLog auditLog)
        {
            Console.WriteLine();
            Console.WriteLine("--- Make Reservation ---");

            Employee employee = SelectEmployee();
            if (employee == null)
            {
                Console.WriteLine("Employee not found.");
                return;
            }

            Equipment equipment = SelectEquipment();
            if (equipment == null)
            {
                Console.WriteLine("Equipment not found.");
                return;
            }

            Reservation reservation = reservationManager.SubmitReservation(
                employee,
                equipment,
                DateTime.Now.AddDays(1)
            );

            if (reservation != null)
            {
                reservationManager.ApproveReservation(reservation);

                // Log reservation activity
                auditLog.RecordAction(
                    "User " + SessionManager.CurrentUser.Username +
                    " created and approved a reservation. Employee ID: " + employee.EmployeeID +
                    ", Equipment ID: " + equipment.EquipmentID
                );
            }
        }

        /// <summary>
        /// Handles report generation with role-based access control.
        /// </summary>
        private static void HandleReport(
            ReportManager reportManager,
            AuditLog auditLog)
        {
            // Enforce role-based access restriction
            if (SessionManager.CurrentUser.Role != "Supervisor" &&
                SessionManager.CurrentUser.Role != "Admin")
            {
                Console.WriteLine("Access denied. Only Supervisors and Admins can generate reports.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("--- Generate Report ---");
            Console.WriteLine("1. Usage Report");
            Console.WriteLine("2. Loss Report");
            Console.WriteLine("3. Reservation Report");
            Console.Write("Select report type: ");

            string reportChoice = Console.ReadLine();

            switch (reportChoice)
            {
                case "1":
                    reportManager.GenerateReport("usage");
                    auditLog.RecordAction("User " + SessionManager.CurrentUser.Username + " generated a usage report.");
                    break;

                case "2":
                    reportManager.GenerateReport("loss");
                    auditLog.RecordAction("User " + SessionManager.CurrentUser.Username + " generated a loss report.");
                    break;

                case "3":
                    reportManager.GenerateReport("reservation");
                    auditLog.RecordAction("User " + SessionManager.CurrentUser.Username + " generated a reservation report.");
                    break;

                default:
                    Console.WriteLine("Invalid report selection.");
                    break;
            }
        }

        /// <summary>
        /// Prompts user to select an employee by ID.
        /// </summary>
        private static Employee SelectEmployee()
        {
            Console.Write("Enter Employee ID: ");
            int employeeId;

            if (!int.TryParse(Console.ReadLine(), out employeeId))
            {
                Console.WriteLine("Invalid employee ID.");
                return null;
            }

            return DatabaseManager.Instance.Employees
                .FirstOrDefault(e => e.EmployeeID == employeeId);
        }

        /// <summary>
        /// Prompts user to select equipment by ID.
        /// </summary>
        private static Equipment SelectEquipment()
        {
            Console.Write("Enter Equipment ID: ");
            int equipmentId;

            if (!int.TryParse(Console.ReadLine(), out equipmentId))
            {
                Console.WriteLine("Invalid equipment ID.");
                return null;
            }

            return DatabaseManager.Instance.EquipmentItems
                .FirstOrDefault(e => e.EquipmentID == equipmentId);
        }
    }
}