using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS.Models
{
    public class Reservation
    {
        public int ReservationID { get; set; }
        public int EmployeeID { get; set; }
        public int EquipmentID { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime PickupDate { get; set; }
        public string ReservationStatus { get; set; }

        public Reservation()
        {
            ReservationStatus = "Pending";
        }

        public void CreateReservation()
        {
            ReservationStatus = "Pending";
            Console.WriteLine("Reservation created.");
        }

        public void ConfirmReservation()
        {
            ReservationStatus = "Confirmed";
            Console.WriteLine("Reservation confirmed.");
        }

        public void CancelReservation()
        {
            ReservationStatus = "Canceled";
            Console.WriteLine("Reservation canceled.");
        }
    }
}