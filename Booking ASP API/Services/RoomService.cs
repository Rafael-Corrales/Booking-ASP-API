using System.Collections.Generic;
using System.Linq;
using Booking_ASP_API.Models;

namespace Booking_ASP_API.Services 
{
     public class RoomService
    {
        static List<Room> Rooms { get; }
        static int nextId = 2;

        static RoomService()
        {
            Rooms = new List<Room>
            {
                new Room { idRoom = 1, roomStatus = true, categoryRoom = "Triple", roomDetails="TV Control, WiFi, 2 Bedrooms", roomNumber =101 },
                new Room { idRoom = 2, roomStatus = false, categoryRoom = "Triple", roomDetails="TV Control, WiFi, 2 Bedrooms", roomNumber =102 }

            };
        }
             
        public List<Room> AllRooms()
        {
            List<Room> AllTheRooms = Rooms;           
            return AllTheRooms;
        }

        public Room RoomById(int id)
        {
            Room myRoom = Rooms.FirstOrDefault(s => s.idRoom == id);
            return myRoom;
        }

        public List<Room> ActiveRooms()
        {
            List<Room> AllTheRooms = Rooms.Where(s => s.roomStatus).ToList();
            return AllTheRooms;
        }

        public void AddRoom(Room myRoom)
        {
            myRoom.idRoom = nextId + 1;
            Room existsRoom = Rooms.FirstOrDefault(s => s.roomNumber == myRoom.roomNumber);
            if(existsRoom == null)
                nextId = nextId + 1;
                Rooms.Add(myRoom);
        }

        public void UpdateRoom(Room myRoom)
        {
            var index = Rooms.FindIndex(s => s.idRoom == myRoom.idRoom);
            if (index == -1)
                return;

            Room existsRoom = Rooms.FirstOrDefault(s => s.roomNumber == myRoom.roomNumber && s.idRoom != myRoom.idRoom);
            if (existsRoom == null)
                Rooms[index] = myRoom;
        }


        public void DeleteRoom(Room myRoom)
        {
            var room = Rooms.FirstOrDefault(s => s.idRoom == myRoom.idRoom);
            if (room == null)
                return;
            Rooms.Remove(room);
        }
    }
}