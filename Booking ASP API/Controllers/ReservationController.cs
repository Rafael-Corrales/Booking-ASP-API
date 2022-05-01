using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Booking_ASP_API.Models;
using Booking_ASP_API.Services;
namespace Booking_ASP_API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        // Create an ReservationService instance to use their methods and properties
        ReservationService reservationService;
        public ReservationController()
        {
            reservationService = new ReservationService();
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
