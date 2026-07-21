using HotelReservationAPI.MODEL;

using HotelReservationAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace HotelReservationAPI.Controllers
{

    [ApiController]
    [Route("api/payment")]

    public class PaymentController : ControllerBase
    {

        private readonly IPaymentService service;



        public PaymentController(IPaymentService service)
        {
            this.service = service;
        }




        [HttpPost]

        public IActionResult Add(Payment payment)
        {

            return Ok(service.Add(payment));

        }




        [HttpGet]

        public IActionResult GetAll()
        {

            return Ok(service.GetAll());

        }




        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {

            var payment = service.GetById(id);


            if (payment == null)
                return NotFound("Payment Not Found");


            return Ok(payment);

        }




        [HttpPut("{id}")]

        public IActionResult Update(int id, Payment payment)
        {

            payment.PaymentID = id;


            return Ok(service.Update(payment));

        }




        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {

            return Ok(service.Delete(id));

        }


    }

}