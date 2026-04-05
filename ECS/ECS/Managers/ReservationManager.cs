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

        public Reservation SubmitReservation(DateTime pickupDate)
        {
            Reservation reservation = new Reservation();
            reservation.ReservationID = ReservationList.Count + 1;
            reservation.RequestDate = DateTime.Now;
            reservation.PickupDate = pickupDate;

            reservation.CreateReservation();
            ReservationList.Add(reservation);
            DatabaseManager.Instance.SaveRecord("Reservation");

            return reservation;
        }

        public void ApproveReservation(Reservation reservation)
        {
            reservation.ConfirmReservation();
            DatabaseManager.Instance.UpdateRecord("Reservation");
        }

        public void CancelReservation(Reservation reservation)
        {
            reservation.CancelReservation();
            DatabaseManager.Instance.UpdateRecord("Reservation");
        }
    }
}