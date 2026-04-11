using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECS.Models;

namespace ECS.Services
{
    public sealed class DatabaseManager
    {
        private static readonly DatabaseManager _instance = new DatabaseManager();

        public string ConnectionStatus { get; private set; }
        public string DatabaseName { get; private set; }

        public List<Employee> Employees { get; private set; }
        public List<Equipment> EquipmentItems { get; private set; }
        public List<Reservation> Reservations { get; private set; }
        public List<Transaction> Transactions { get; private set; }
        public List<string> AuditEntries { get; private set; }
        public List<UserAccount> UserAccounts { get; private set; }

        private DatabaseManager()
        {
            ConnectionStatus = "Closed";
            DatabaseName = "ECSDatabase";
            Employees = new List<Employee>();
            EquipmentItems = new List<Equipment>();
            Reservations = new List<Reservation>();
            Transactions = new List<Transaction>();
            AuditEntries = new List<string>();
            UserAccounts = new List<UserAccount>();
        }

        public static DatabaseManager Instance
        {
            get { return _instance; }
        }

        public void OpenConnection()
        {
            ConnectionStatus = "Open";
            Console.WriteLine("Connection to " + DatabaseName + " is now open.");
        }

        public void SaveRecord(string recordType)
        {
            Console.WriteLine(recordType + " record saved to database.");
        }

        public void RetrieveRecord(string recordType)
        {
            Console.WriteLine("Retrieving " + recordType + " record from database.");
        }

        public void UpdateRecord(string recordType)
        {
            Console.WriteLine(recordType + " record updated in database.");
        }
    }
}