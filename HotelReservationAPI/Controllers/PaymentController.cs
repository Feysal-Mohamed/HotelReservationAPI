using HotelReservationAPI.MODEL;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationAPI.Controllers
{
    [ApiController]
    [Route("api/payment")]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentHelper helper = new PaymentHelper();

        // ================= ADD =================
        [HttpPost]
        public IActionResult Add(Payment p)
        {
            string result = helper.AddPayment(p);

            if (result.StartsWith("Success"))
                return Ok(result);

            return BadRequest(result);
        }

        // ================= GET ALL =================
        [HttpGet]
        public IActionResult GetAll()
        {
            var (payments, status) = helper.GetAllPayments();

            if (status == "Success")
                return Ok(payments);

            return BadRequest(status);
        }

        // ================= GET BY ID =================
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var (payment, status) = helper.GetPaymentById(id);

            if (status != "Success")
                return BadRequest(status);

            if (payment == null)
                return NotFound("Payment Not Found");

            return Ok(payment);
        }

        // ================= UPDATE =================
        [HttpPut("{id}")]
        public IActionResult Update(int id, Payment p)
        {
            p.PaymentID = id;
            string result = helper.UpdatePayment(p);

            if (result.StartsWith("Success"))
                return Ok(result);

            return BadRequest(result);
        }

        // ================= DELETE =================
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string result = helper.DeletePayment(id);

            if (result.StartsWith("Success"))
                return Ok(result);

            return BadRequest(result);
        }
    }
}
