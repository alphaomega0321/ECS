using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECS.Models;
using ECS.Services;

namespace ECS.Managers
{
    public class ReservationManager
    {
        public List<Reservation> ReservationList
        {
            get { return DatabaseManager.Instance.Reservations; }
        }

        public Reservation SubmitReservation(Employee employee, Equipment equipment, DateTime pickupDate)
        {
            if (equipment.Status != "Available")
            {
                Console.WriteLine("Reservation stopped. Equipment is not available.");
                return null;
            }

            Reservation reservation = new Reservation();
            reservation.ReservationID = ReservationList.Count + 1;
            reservation.EmployeeID = employee.EmployeeID;
            reservation.EquipmentID = equipment.EquipmentID;
            reservation.RequestDate = DateTime.Now;
            reservation.PickupDate = pickupDate;

            reservation.CreateReservation();
            ReservationList.Add(reservation);
            DatabaseManager.Instance.SaveRecord("Reservation");

            return reservation;
        }

        public void ApproveReservation(Reservation reservation)
        {
            if (reservation == null)
            {
                return;
            }

            reservation.ConfirmReservation();
            DatabaseManager.Instance.UpdateRecord("Reservation");
        }

        public void CancelReservation(Reservation reservation)
        {
            if (reservation == null)
            {
                return;
            }

            reservation.CancelReservation();
            DatabaseManager.Instance.UpdateRecord("Reservation");
        }

        internal Reservation SubmitReservation(DateTime dateTime)
        {
            throw new NotImplementedException();
        }
    }
}