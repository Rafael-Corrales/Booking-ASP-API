using System;
using System.Collections.Generic;
using System.Linq;
using Booking_ASP_API.Models;

namespace Booking_ASP_API.Services
{
    public class ReservationService
    {
        // Create variables to use them in the class
        static List<Reservation> Reservations { get; }
        static int nextId = 0;
        RoomService roomService = new RoomService();
        static ReservationService()
        {
            Reservations = new List<Reservation>
            {

            };
        }

        public List<Reservation> AllReservations()
        {
            List<Reservation> AllTheReservations = Reservations;
            return AllTheReservations;
        }
        // Get reservations between specific dates
        public List<Reservation> ReservationsBetweenDates(DateTime startDate, DateTime endDate)
        {

            // Query
            List<Room> RoomsActives = roomService.ActiveRooms();
            IEnumerable<int> IdRoomsActives = RoomsActives.Select(s => s.idRoom).Distinct();
            // Subquery
            List<Reservation> AllTheReservations = Reservations.Where(s => ((s.checkInDate >= startDate && s.checkOutDate <= startDate) || (s.checkInDate >= endDate && s.checkOutDate <= endDate)) && IdRoomsActives.Contains(s.idRoom)).ToList();
            return AllTheReservations;
        }

        // Get reservations between dates without an specific reservation
        public List<Reservation> ReservationsBetweenDatesWithReservation(DateTime startDate, DateTime endDate, int idReservation)
        {
            // Query
            List<Room> RoomsActives = roomService.ActiveRooms();
            IEnumerable<int> IdRoomsActives = RoomsActives.Select(s => s.idRoom).Distinct();
            // Subquery
            List<Reservation> AllTheReservations = Reservations.Where(s => ((s.checkInDate >= startDate && s.checkOutDate <= startDate) || (s.checkInDate >= endDate && s.checkOutDate <= endDate)) && IdRoomsActives.Contains(s.idRoom) && s.idReservation != idReservation).ToList();
            return AllTheReservations;
        }

        // Get an specific reservation by IdReservation
        public Reservation ReservationById(int id)
        {
            Reservation myReservation = Reservations.FirstOrDefault(s => s.idReservation == id);
            return myReservation;
        }
        // Add a new reservation
        public void AddReservation(Reservation myReservation)
        {
            myReservation.idReservation = nextId +1;
            nextId = nextId + 1;
            var res = roomService.RoomById(myReservation.idRoom);
            myReservation.room = res;
            Reservations.Add(myReservation);
        }

        // Update an specific reservation
        public void UpdateReservation(Reservation myReservation)
        {
            var index = Reservations.FindIndex(s => s.idReservation == myReservation.idReservation);
            var res = roomService.RoomById(myReservation.idRoom);
            if (index == -1)
                return;

            myReservation.room = res;
            Reservations[index] = myReservation;
        }
        // Delete an specific reservation
        public void DeleteReservation(Reservation myReservation)
        {
            var reservation = Reservations.FirstOrDefault(s => s.idReservation == myReservation.idReservation);

            Reservations.Remove(reservation);
        }
    }
}
