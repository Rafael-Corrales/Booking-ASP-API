using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Booking_ASP_API.Models;
using Booking_ASP_API.Services;
namespace Booking_ASP_API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {

        RoomService roomService;
        public RoomController()
        {
            roomService = new RoomService();
        }

        // GET all action
        [HttpGet("[action]")]
        public IActionResult GetAll()
        {
            List<Room> AllRooms = new List<Room>();
            try
            {
                AllRooms = roomService.AllRooms();

            }
            catch (Exception e)
            {
                return BadRequest($"Error: {e.Message}");
            }
            return Ok(AllRooms);
        }

        // GET by Id action
        [HttpGet("[action]/{id}/")]
        public IActionResult GetById(int id)
        {
            Room myRoom;
            try
            {
                myRoom = roomService.RoomById(id);
            }
            catch (Exception e)
            {
                return BadRequest($"Error: {e.Message}");
            }
            return Ok(myRoom);
        }

        [HttpGet("[action]")]
        public IActionResult GetActives()
        {
            List<Room> ActiveRooms = new List<Room>();
            try
            {
                ActiveRooms = roomService.ActiveRooms();

            }
            catch (Exception e)
            {
                return BadRequest($"Error: {e.Message}");
            }
            return Ok(ActiveRooms);
        }



        // GET by Id action
        [HttpPost("[action]")]
        public IActionResult Insert(Room myRoom)
        {
            try
            {
                roomService.AddRoom(myRoom);
            }
            catch (Exception e)
            {
                return BadRequest($"Error: {e.Message}");
            }
            return Ok(myRoom);
        }


        // GET by Id action
        [HttpPut("[action]")]
        public IActionResult Update(Room myRoom)
        {
            try
            {
                roomService.UpdateRoom(myRoom);
            }
            catch (Exception e)
            {
                return BadRequest($"Error: {e.Message}");
            }
            return Ok(myRoom);
        }

        [HttpDelete("[action]")]
        public IActionResult Delete(Room myRoom)
        {
            try
            {
                roomService.DeleteRoom(myRoom);
            }
            catch (Exception e)
            {
                return BadRequest($"Error: {e.Message}");
            }
            return Ok(myRoom);
        }
    }

}
