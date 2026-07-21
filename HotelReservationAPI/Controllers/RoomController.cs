using HotelReservationAPI.MODEL;
using HotelReservationAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace HotelReservationAPI.Controllers
{

    [ApiController]
    [Route("api/room")]

    public class RoomController : ControllerBase
    {

        private readonly IRoomService service;


        public RoomController(IRoomService service)
        {
            this.service = service;
        }



        [HttpPost]
        public IActionResult Add(Room room)
        {
            return Ok(service.Add(room));
        }



        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(service.GetAll());
        }



        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var room = service.GetById(id);

            if (room == null)
                return NotFound();

            return Ok(room);
        }



        [HttpPut("{id}")]
        public IActionResult Update(int id, Room room)
        {
            room.RoomID = id;

            return Ok(service.Update(room));
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(service.Delete(id));
        }

    }

}