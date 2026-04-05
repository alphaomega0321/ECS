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

        public Equipment SearchEquipment(string equipmentName)
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
    }
}