using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using HotelReservationAPI.MODEL;

namespace HotelReservationAPI.Controllers
{
    [ApiController]
    [Route("api/payment")]
    public class PaymentController : ControllerBase
    {
        PaymentHelper helper = new PaymentHelper();

        // ================= ADD =================
        [HttpPost]
        public IActionResult Add(Payment p)
        {
            return Ok(helper.AddPayment(p));
        }

        // ================= GET ALL =================
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(helper.GetAllPayments());
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
                var payment = helper.GetPaymentById(id);

                if (payment == null)
                    return NotFound("Payment Not Found");

                return Ok(payment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ================= UPDATE =================
        [HttpPut("{id}")]
        public IActionResult Update(int id, Payment p)
        {
            p.PaymentID = id;
            return Ok(helper.UpdatePayment(p));
        }

        // ================= DELETE =================
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(helper.DeletePayment(id));
        }
    }
}