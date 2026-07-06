using HotelReservationAPI.MODEL;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationAPI.Controllers
{
    [ApiController]
    [Route("api/room")]
    public class RoomController : ControllerBase
    {
        private readonly RoomHelper helper = new RoomHelper();

        // ================= ADD =================
        [HttpPost]
        public IActionResult Add(Room room)
        {
            string result = helper.AddRoom(room);

            if (result.StartsWith("Success"))
                return Ok(result);

            return BadRequest(result);
        }

        // ================= GET ALL =================
        [HttpGet]
        public IActionResult GetAll()
        {
            var (rooms, status) = helper.GetAllRooms();

            if (status == "Success")
                return Ok(rooms);

            return BadRequest(status);
        }

        // ================= GET BY ID =================
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var (room, status) = helper.GetRoomById(id);

            if (status != "Success")
                return BadRequest(status);

            if (room == null)
                return NotFound("Room Not Found");

            return Ok(room);
        }

        // ================= UPDATE =================
        [HttpPut("{id}")]
        public IActionResult Update(int id, Room room)
        {
            room.RoomID = id;
            string result = helper.UpdateRoom(room);

            if (result.StartsWith("Success"))
                return Ok(result);

            return BadRequest(result);
        }

        // ================= DELETE =================
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string result = helper.DeleteRoom(id);

            if (result.StartsWith("Success"))
                return Ok(result);

            return BadRequest(result);
        }
    }
}
