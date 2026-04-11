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
    public class ReservationReport : IReport
    {
        public void Generate()
        {
            Console.WriteLine("Generating reservation activity report...");

            if (DatabaseManager.Instance.Reservations.Count == 0)
            {
                Console.WriteLine("No reservation records found.");
                return;
            }

            foreach (Reservation reservation in DatabaseManager.Instance.Reservations)
            {
                Console.WriteLine(
                    "Reservation ID: " + reservation.ReservationID +
                    " | Employee ID: " + reservation.EmployeeID +
                    " | Equipment ID: " + reservation.EquipmentID +
                    " | Status: " + reservation.ReservationStatus
                );
            }
        }
    }
}