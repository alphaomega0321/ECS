using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ECS.Models
{
    public class Equipment
    {
        public int EquipmentID { get; set; }
        public string EquipmentName { get; set; }
        public string EquipmentType { get; set; }
        public string Status { get; set; }
        public string BarcodeValue { get; set; }
        public string Location { get; set; }

        public Equipment()
        {
            EquipmentName = string.Empty;
            EquipmentType = string.Empty;
            Status = "Available";
            BarcodeValue = string.Empty;
            Location = string.Empty;
        }

        public void UpdateStatus(string newStatus)
        {
            Status = newStatus;
            Console.WriteLine("Equipment '" + EquipmentName + "' status updated to " + Status + ".");
        }

        public void AssignToEmployee(Employee employee)
        {
            Status = "Checked Out";
            Console.WriteLine("Equipment '" + EquipmentName + "' assigned to " + employee.EmployeeName + ".");
        }

        public void ReturnToInventory()
        {
            Status = "Available";
            Console.WriteLine("Equipment '" + EquipmentName + "' returned to inventory.");
        }
    }
}