using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking_ASP_API.Models
{
    // Room Model
    public class Room
    {
        public int idRoom { get; set; }
        public int roomNumber { get; set; }
        public string? roomDetails { get; set; }
        public string? categoryRoom { get; set; }
        public bool roomStatus { get; set; }
    }
}
