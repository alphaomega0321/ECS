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
        /// <summary>
        /// Creates a new equipment reservation for a selected employee and equipment item.
        /// The reservation is only allowed if the equipment is currently available.
        /// </summary>
        public Reservation SubmitReservation(Employee employee, Equipment equipment, DateTime pickupDate)
        {
            // Prevent reservations for equipment that is already checked out or unavailable
            if (equipment.Status != "Available")
            {
                Console.WriteLine("Reservation stopped. Equipment is not available.");
                return null;
            }

            // Create reservation record and connect it to employee and equipment
            Reservation reservation = new Reservation();
            reservation.ReservationID = DatabaseManager.Instance.Reservations.Count + 1;
            reservation.EmployeeID = employee.EmployeeID;
            reservation.EquipmentID = equipment.EquipmentID;
            reservation.RequestDate = DateTime.Now;
            reservation.PickupDate = pickupDate;

            // Set reservation status and store record
            reservation.CreateReservation();
            DatabaseManager.Instance.Reservations.Add(reservation);
            DatabaseManager.Instance.SaveRecord("Reservation");

            return reservation;
        }

        /// <summary>
        /// Approves an existing reservation.
        /// This allows depot staff to confirm equipment pickup.
        /// </summary>
        public void ApproveReservation(Reservation reservation)
        {
            // Avoid errors if reservation creation failed
            if (reservation == null)
            {
                return;
            }

            reservation.ConfirmReservation();
            DatabaseManager.Instance.UpdateRecord("Reservation");
        }

        /// <summary>
        /// Cancels an existing reservation.
        /// This supports reservation management if equipment is no longer needed.
        /// </summary>
        public void CancelReservation(Reservation reservation)
        {
            // Avoid errors if no reservation was provided
            if (reservation == null)
            {
                return;
            }

            reservation.CancelReservation();
            DatabaseManager.Instance.UpdateRecord("Reservation");
        }
    }
}