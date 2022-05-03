using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Booking_ASP_API.Models;
using Booking_ASP_API.Services;
using System.Linq;

namespace Booking_ASP_API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        // Create an ReservationService instance to use their methods and properties
        ReservationService reservationService;
        RoomService roomService;
        public ReservationController()
        {
            reservationService = new ReservationService();
            roomService = new RoomService();
        }

        // GET all action
        [HttpGet("[action]")]
        public IActionResult GetAll()
        {
            List<Reservation> AllReservations = new List<Reservation>();
            try
            {
                AllReservations = reservationService.AllReservations();

            }
            catch (Exception e)
            {
                return BadRequest($"Error: {e.Message}");
            }
            return Ok(AllReservations);
        }


        // GET reservations between dates action
        [HttpGet("[action]/{startDate}/{endDate}/")]
        public IActionResult GetReservationsBetwwenDates(DateTime startDate, DateTime endDate)
        {
            List<Reservation> AllReservations = new List<Reservation>();
            try
            {
                AllReservations = reservationService.ReservationsBetweenDates(startDate, endDate);
            }
            catch (Exception e)
            {
                return BadRequest($"Error: {e.Message}");
            }
            return Ok(AllReservations);
        }


        [HttpGet("[action]/{startDate}/{endDate}/")]
        public IActionResult CheckAvailability(DateTime startDate, DateTime endDate)
        {
            List<Room> roomsActive = this.roomService.ActiveRooms();
            List<Reservation> AllReservations = new List<Reservation>();
            try
            {
                AllReservations = reservationService.ReservationsBetweenDates(startDate, endDate);
                IEnumerable<int> idRoomsAvailable = AllReservations.Select(s => s.idRoom).Distinct();
                roomsActive = roomsActive.Where(s => !idRoomsAvailable.Contains(s.idRoom)).ToList();
            }
            catch (Exception e)
            {
                return BadRequest($"Error: {e.Message}");
            }
            return Ok(roomsActive);
        }

        // GET reservations between dates without an specific reservation action 
        [HttpGet("[action]/{startDate}/{endDate}/{idReservation}")]
        public IActionResult GetReservationsBetwwenDatesWithReservation(DateTime startDate, DateTime endDate, int idReservation)
        {
            List<Reservation> AllReservations = new List<Reservation>();
            try
            {
                AllReservations = reservationService.ReservationsBetweenDatesWithReservation(startDate, endDate, idReservation);

            }
            catch (Exception e)
            {
                return BadRequest($"Error: {e.Message}");
            }
            return Ok(AllReservations);
        }

        // GET by Id action
        [HttpGet("[action]/{id}/")]
        public IActionResult GetById(int id)
        {
            Reservation myReservation;
            try
            {
                myReservation = reservationService.ReservationById(id);
            }
            catch (Exception e)
            {
                return BadRequest($"Error: {e.Message}");
            }
            return Ok(myReservation);
        }


        // POST Inser action
        [HttpPost("[action]")]
        public IActionResult Insert(Reservation myReservation)
        {
            try
            {
                // Validate the CheckOut is greater than checkin"

                if (myReservation.checkInDate > myReservation.checkOutDate)
                {
                    return BadRequest($"Error: The checkout date must be greater than checkin date");
                }

                // Get the days of difference to meet this requirement: "All reservations start at least the next day of booking"
                TimeSpan t = myReservation.checkInDate - myReservation.bookingDate;
                double nbrOfDays = t.TotalDays;
                if (nbrOfDays > 1)
                {
                    return BadRequest($"Error: All reservations start at least the next day of booking");
                }

                // Get the days of difference to meet this requirement: "The stay can’t be longer than 3 days"
                TimeSpan t2 = myReservation.checkOutDate - myReservation.checkInDate;
                double nbrOfDays2 = t2.TotalDays;
                if (nbrOfDays2 > 3)
                {
                    return BadRequest($"Error: The stay can’t be longer than 3 days ");
                }

                // Get the days of difference to meet this requirement: "The stay can’t be reserved more than 30 days in advance"

                DateTime dateWith30days = DateTime.Today.AddDays(30);
                if (myReservation.checkInDate > dateWith30days)
                {
                    return BadRequest($"Error: The stay can’t be reserved more than 30 days in advance.");
                }
                reservationService.AddReservation(myReservation);
            }
            catch (Exception e)
            {
                return BadRequest($"Error: {e.Message}");
            }
            return Ok(myReservation);
        }

        // PUT Update action
        [HttpPut("[action]")]
        public IActionResult Update(Reservation myReservation)
        {
            try
            {
                if (myReservation.checkInDate > myReservation.checkOutDate)
                {
                    return BadRequest($"Error: The checkout date must be greater than checkin date");
                }

                // Get the days of difference to meet this requirement: "All reservations start at least the next day of booking"
                TimeSpan t = myReservation.checkInDate - myReservation.bookingDate;
                double nbrOfDays = t.TotalDays;
                if (nbrOfDays > 1)
                {
                    return BadRequest($"Error: All reservations start at least the next day of booking");
                }


                TimeSpan t2 = myReservation.checkOutDate - myReservation.checkInDate;
                double nbrOfDays2 = t2.TotalDays;
                if (nbrOfDays > 3)
                {
                    return BadRequest($"Error: The stay can’t be longer than 3 days ");
                }


                DateTime dateWith30days = DateTime.Today.AddDays(30);
                if (myReservation.checkInDate > dateWith30days)
                {
                    return BadRequest($"Error: The stay can’t be reserved more than 30 days in advance.");
                }
                reservationService.UpdateReservation(myReservation);
            }
            catch (Exception e)
            {
                return BadRequest($"Error: {e.Message}");
            }
            return Ok(myReservation);
        }

        // Delete action
        [HttpDelete("[action]")]
        public IActionResult Delete(Reservation myReservation)
        {
            try
            {
                reservationService.DeleteReservation(myReservation);
            }
            catch (Exception e)
            {
                return BadRequest($"Error: {e.Message}");
            }
            return Ok(myReservation);
        }
    }

}
