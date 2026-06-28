using HotelReservationAPI.Helper;
using HotelReservationAPI.MODEL;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HotelReservationAPI.Controllers
{
    [ApiController]
    [Route("api/reservation")]
    public class ReservationController : ControllerBase
    {
        ReservationHelper helper = new ReservationHelper();

        // ================= ADD =================
        [HttpPost]
        public IActionResult Add(Reservation r)
        {
            return Ok(helper.AddReservation(r));
        }

        // ================= GET ALL =================
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(helper.GetAllReservations());
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
                var reservation = helper.GetReservationById(id);

                if (reservation == null)
                    return NotFound("Reservation Not Found");

                return Ok(reservation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ================= UPDATE =================
        [HttpPut("{id}")]
        public IActionResult Update(int id, Reservation r)
        {
            r.ReservationID = id;
            return Ok(helper.UpdateReservation(r));
        }

        // ================= DELETE =================
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(helper.DeleteReservation(id));
        }
    }
}