using HotelReservationAPI.MODEL;
using HotelReservationAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace HotelReservationAPI.Controllers
{

    [ApiController]
    [Route("api/reservation")]

    public class ReservationController : ControllerBase
    {

        private readonly IReservationService service;



        public ReservationController(IReservationService service)
        {
            this.service = service;
        }



        [HttpPost]

        public IActionResult Add(Reservation r)
        {
            return Ok(service.Add(r));
        }




        [HttpGet]

        public IActionResult GetAll()
        {
            return Ok(service.GetAll());
        }




        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {

            var reservation = service.GetById(id);


            if (reservation == null)
                return NotFound("Reservation Not Found");


            return Ok(reservation);

        }




        [HttpPut("{id}")]

        public IActionResult Update(int id, Reservation r)
        {

            r.ReservationID = id;


            return Ok(service.Update(r));

        }




        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            return Ok(service.Delete(id));
        }


    }

}