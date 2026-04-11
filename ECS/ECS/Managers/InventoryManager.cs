using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECS.Models;
using ECS.Services;

namespace ECS.Managers
{
    public class InventoryManager
    {
        public List<Equipment> EquipmentList
        {
            get { return DatabaseManager.Instance.EquipmentItems; }
        }

        public Equipment SearchEquipmentById(int equipmentId)
        {
            return EquipmentList.FirstOrDefault(e => e.EquipmentID == equipmentId);
        }

        public Equipment SearchEquipmentByName(string equipmentName)
        {
            return EquipmentList.FirstOrDefault(
                e => e.EquipmentName.Equals(equipmentName, StringComparison.OrdinalIgnoreCase)
            );
        }

        public void UpdateAvailability(Equipment equipment, string status)
        {
            equipment.UpdateStatus(status);
            DatabaseManager.Instance.UpdateRecord("Equipment");
        }

        public void UpdateConditionStatus(Equipment equipment, string condition)
        {
            equipment.UpdateStatus(condition);
            DatabaseManager.Instance.UpdateRecord("Equipment Condition");
        }

        public void DisplayAllEquipment()
        {
            Console.WriteLine();
            Console.WriteLine("Available Equipment Records:");
            foreach (Equipment equipment in EquipmentList)
            {
                Console.WriteLine(
                    "ID: " + equipment.EquipmentID +
                    " | Name: " + equipment.EquipmentName +
                    " | Status: " + equipment.Status +
                    " | Required Skill: " + equipment.RequiredSkill
                );
            }
        }
    }
}