using HotelReservationAPI.MODEL;
using HotelReservationAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace HotelReservationAPI.Controllers
{

    [ApiController]
    [Route("api/customers")]

    public class CustomerController : ControllerBase
    {


        private readonly ICustomerService service;



        public CustomerController(ICustomerService service)
        {
            this.service = service;
        }



        [HttpPost]
        public IActionResult Add(Customer customer)
        {

            return Ok(service.Add(customer));

        }



        [HttpGet]
        public IActionResult GetAll()
        {

            return Ok(service.GetAll());

        }



        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {

            var customer = service.GetById(id);


            if (customer == null)
                return NotFound("Customer Not Found");


            return Ok(customer);

        }




        [HttpPatch("{id}")]

        public IActionResult Update(int id, Customer customer)
        {

            customer.CustomerID = id;


            return Ok(service.Update(customer));

        }




        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {

            return Ok(service.Delete(id));

        }



    }

}