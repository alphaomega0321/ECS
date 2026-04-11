using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECS.Models;

namespace ECS.Models
{
    public class Equipment
    {
        public int EquipmentID { get; set; }
        public string EquipmentName { get; set; }
        public string EquipmentType { get; set; }
        public string RequiredSkill { get; set; }
        public string Status { get; set; }
        public string BarcodeValue { get; set; }
        public string Location { get; set; }
        public int? AssignedEmployeeID { get; set; }

        public Equipment()
        {
            EquipmentName = string.Empty;
            EquipmentType = string.Empty;
            RequiredSkill = string.Empty;
            Status = "Available";
            BarcodeValue = string.Empty;
            Location = string.Empty;
            AssignedEmployeeID = null;
        }

        public void UpdateStatus(string newStatus)
        {
            Status = newStatus;
            Console.WriteLine("Equipment '" + EquipmentName + "' status updated to " + Status + ".");
        }

        public void AssignToEmployee(Employee employee)
        {
            AssignedEmployeeID = employee.EmployeeID;
            Status = "Checked Out";
            Console.WriteLine("Equipment '" + EquipmentName + "' assigned to " + employee.EmployeeName + ".");
        }

        public void ReturnToInventory()
        {
            AssignedEmployeeID = null;
            Status = "Available";
            Console.WriteLine("Equipment '" + EquipmentName + "' returned to inventory.");
        }
    }
}