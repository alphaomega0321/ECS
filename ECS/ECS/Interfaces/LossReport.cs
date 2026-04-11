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
    public class LossReport : IReport
    {
        public void Generate()
        {
            Console.WriteLine("Generating equipment loss report...");

            var flaggedEquipment = DatabaseManager.Instance.EquipmentItems
                .Where(e => e.Status == "Lost" || e.Status == "Damaged");

            if (!flaggedEquipment.Any())
            {
                Console.WriteLine("No lost or damaged equipment found.");
                return;
            }

            foreach (Equipment equipment in flaggedEquipment)
            {
                Console.WriteLine(
                    "Equipment ID: " + equipment.EquipmentID +
                    " | Name: " + equipment.EquipmentName +
                    " | Status: " + equipment.Status
                );
            }
        }
    }
}