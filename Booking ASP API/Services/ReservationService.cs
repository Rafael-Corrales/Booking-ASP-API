using System;
using System.Collections.Generic;
using System.Linq;
using Booking_ASP_API.Models;

namespace Booking_ASP_API.Services
{
    public class ReservationService
    {
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

        public List<Reservation> ReservationsBetweenDates(DateTime startDate, DateTime endDate)
        {
            List<Room> RoomsActives = roomService.ActiveRooms();
            IEnumerable<int> IdRoomsActives = RoomsActives.Select(s => s.idRoom).Distinct();
            List<Reservation> AllTheReservations = Reservations.Where(s => ((s.checkInDate >= startDate && s.checkOutDate <= startDate) || (s.checkInDate >= endDate && s.checkOutDate <= endDate)) && IdRoomsActives.Contains(s.idRoom)).ToList();
            return AllTheReservations;
        }

        public List<Reservation> ReservationsBetweenDatesWithReservation(DateTime startDate, DateTime endDate, int idReservation)
        {
            List<Room> RoomsActives = roomService.ActiveRooms();
            IEnumerable<int> IdRoomsActives = RoomsActives.Select(s => s.idRoom).Distinct();
            List<Reservation> AllTheReservations = Reservations.Where(s => ((s.checkInDate >= startDate && s.checkOutDate <= startDate) || (s.checkInDate >= endDate && s.checkOutDate <= endDate)) && IdRoomsActives.Contains(s.idRoom) && s.idReservation != idReservation).ToList();
            return AllTheReservations;
        }

        public Reservation ReservationById(int id)
        {
            Reservation myReservation = Reservations.FirstOrDefault(s => s.idReservation == id);
            return myReservation;
        }

        public void AddReservation(Reservation myReservation)
        {
            myReservation.idReservation = nextId +1;
            nextId = nextId + 1;
            var res = roomService.RoomById(myReservation.idRoom);
            myReservation.room = res;
            Reservations.Add(myReservation);
        }

        public void UpdateReservation(Reservation myReservation)
        {
            var index = Reservations.FindIndex(s => s.idReservation == myReservation.idReservation);
            var res = roomService.RoomById(myReservation.idRoom);
            if (index == -1)
                return;

            myReservation.room = res;
            Reservations[index] = myReservation;
        }

        public void DeleteReservation(Reservation myReservation)
        {
            var reservation = Reservations.FirstOrDefault(s => s.idReservation == myReservation.idReservation);

            Reservations.Remove(reservation);
        }
    }
}
