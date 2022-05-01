using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Booking_ASP_API.Models
{

    // Reservation Model
    public class Reservation
    {
        public int idReservation { get; set; }
        public int idRoom { get; set; }
        public DateTime checkInDate { get; set; }
        public DateTime checkOutDate { get; set; }
        public DateTime bookingDate { get; set; }
        public string customerName { get; set; }
        public string email { get; set; }
        public virtual Room room { get; set; }
    }
}
