using System.Collections.Generic;
using System.Linq;
using Booking_ASP_API.Models;

namespace Booking_ASP_API.Services 
{
     public class RoomService
    {
        // Create variables to use them in the class
        static List<Room> Rooms { get; }
        static int nextId = 2;

        // Create rooms to the model
        static RoomService()
        {
            Rooms = new List<Room>
            {
                new Room { idRoom = 1, roomStatus = true, categoryRoom = "Triple", roomDetails="TV Control, WiFi, 2 Bedrooms", roomNumber =101 },
                new Room { idRoom = 2, roomStatus = false, categoryRoom = "Triple", roomDetails="TV Control, WiFi, 2 Bedrooms", roomNumber =102 }

            };
        }
             
        // Get all the rooms
        public List<Room> AllRooms()
        {
            List<Room> AllTheRooms = Rooms;           
            return AllTheRooms;
        }

        // Get room by IdRoom
        public Room RoomById(int id)
        {
            Room myRoom = Rooms.FirstOrDefault(s => s.idRoom == id);
            return myRoom;
        }
        // Get active rooms
        public List<Room> ActiveRooms()
        {
            List<Room> AllTheRooms = Rooms.Where(s => s.roomStatus).ToList();
            return AllTheRooms;
        }

        // Add a new room
        public void AddRoom(Room myRoom)
        {
            myRoom.idRoom = nextId + 1;
            Room existsRoom = Rooms.FirstOrDefault(s => s.roomNumber == myRoom.roomNumber);
            if(existsRoom == null)
                nextId = nextId + 1;
                Rooms.Add(myRoom);
        }
        // Update an specific room
        public void UpdateRoom(Room myRoom)
        {
            var index = Rooms.FindIndex(s => s.idRoom == myRoom.idRoom);
            if (index == -1)
                return;

            Room existsRoom = Rooms.FirstOrDefault(s => s.roomNumber == myRoom.roomNumber && s.idRoom != myRoom.idRoom);
            if (existsRoom == null)
                Rooms[index] = myRoom;
        }

        // Delete an specific room
        public void DeleteRoom(Room myRoom)
        {
            var room = Rooms.FirstOrDefault(s => s.idRoom == myRoom.idRoom);
            if (room == null)
                return;
            Rooms.Remove(room);
        }
    }
}