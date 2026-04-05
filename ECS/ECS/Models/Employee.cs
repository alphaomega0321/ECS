using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string SkillClassification { get; set; }
        public string Department { get; set; }
        public bool ActiveStatus { get; set; }

        public Employee()
        {
            EmployeeName = string.Empty;
            SkillClassification = string.Empty;
            Department = string.Empty;
            ActiveStatus = true;
        }

        public void RequestEquipment()
        {
            Console.WriteLine(EmployeeName + " is requesting equipment.");
        }

        public void ReserveEquipment()
        {
            Console.WriteLine(EmployeeName + " is reserving equipment.");
        }

        public void ViewAssignedEquipment()
        {
            Console.WriteLine("Displaying assigned equipment for " + EmployeeName + ".");
        }
    }
}
