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
            // Check Customer
            CustomerHelper customerHelper = new CustomerHelper();
            var (customer, customerStatus) = customerHelper.GetCustomerById(r.CustomerID);

            if (customerStatus != "Success")
                return BadRequest(customerStatus);

            if (customer == null)
                return BadRequest("Customer ID does not exist.");

            // Check Room
            RoomHelper roomHelper = new RoomHelper();
            var (room, roomStatus) = roomHelper.GetRoomById(r.RoomID);

            if (roomStatus != "Success")
                return BadRequest(roomStatus);

            if (room == null)
                return BadRequest("Room ID does not exist.");

            // Add Reservation
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

            // Check Customer
            CustomerHelper customerHelper = new CustomerHelper();
            var (customer, customerStatus) = customerHelper.GetCustomerById(r.CustomerID);

            if (customerStatus != "Success")
                return BadRequest(customerStatus);

            if (customer == null)
                return BadRequest("Customer ID does not exist.");

            // Check Room
            RoomHelper roomHelper = new RoomHelper();
            var (room, roomStatus) = roomHelper.GetRoomById(r.RoomID);

            if (roomStatus != "Success")
                return BadRequest(roomStatus);

            if (room == null)
                return BadRequest("Room ID does not exist.");

            // Update Reservation
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
