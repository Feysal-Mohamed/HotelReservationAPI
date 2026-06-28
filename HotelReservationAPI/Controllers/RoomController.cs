using Microsoft.AspNetCore.Mvc;
using HotelReservationAPI.MODEL;
using System;

namespace HotelReservationAPI.Controllers
{
    [ApiController]
    [Route("api/room")]
    public class RoomController : ControllerBase
    {
        RoomHelper helper = new RoomHelper();

        // ================= ADD =================
        [HttpPost]
        public IActionResult Add(Room room)
        {
            return Ok(helper.AddRoom(room));
        }

        // ================= GET ALL =================
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(helper.GetAllRooms());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ================= GET BY ID =================
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var room = helper.GetRoomById(id);

                if (room == null)
                    return NotFound("Room Not Found");

                return Ok(room);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ================= UPDATE =================
        [HttpPut("{id}")]
        public IActionResult Update(int id, Room room)
        {
            room.RoomID = id;
            return Ok(helper.UpdateRoom(room));
        }

        // ================= DELETE =================
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(helper.DeleteRoom(id));
        }
    }
}