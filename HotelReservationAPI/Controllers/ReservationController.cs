using HotelReservationAPI.Helper;
using HotelReservationAPI.MODEL;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationAPI.Controllers
{
    [ApiController]
    [Route("api/reservation")]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationHelper helper = new ReservationHelper();

        // ================= ADD =================
        [HttpPost]
        public IActionResult Add(Reservation r)
        {
            string result = helper.AddReservation(r);

            if (result.StartsWith("Success"))
                return Ok(result);

            return BadRequest(result);
        }

        // ================= GET ALL =================
        [HttpGet]
        public IActionResult GetAll()
        {
            var (reservations, status) = helper.GetAllReservations();

            if (status == "Success")
                return Ok(reservations);

            return BadRequest(status);
        }

        // ================= GET BY ID =================
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var (reservation, status) = helper.GetReservationById(id);

            if (status != "Success")
                return BadRequest(status);

            if (reservation == null)
                return NotFound("Reservation Not Found");

            return Ok(reservation);
        }

        // ================= UPDATE =================
        [HttpPut("{id}")]
        public IActionResult Update(int id, Reservation r)
        {
            r.ReservationID = id;
            string result = helper.UpdateReservation(r);

            if (result.StartsWith("Success"))
                return Ok(result);

            return BadRequest(result);
        }

        // ================= DELETE =================
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string result = helper.DeleteReservation(id);

            if (result.StartsWith("Success"))
                return Ok(result);

            return BadRequest(result);
        }
    }
}
