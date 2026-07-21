using HotelReservationAPI.MODEL;
using HotelReservationAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace HotelReservationAPI.Controllers
{

    [ApiController]
    [Route("api/user")]

    public class UserController : ControllerBase
    {

        private readonly IUserService service;



        public UserController(IUserService service)
        {
            this.service = service;
        }




        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            return Ok(service.Register(user));
        }




        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {

            var result = service.Login(request);


            if (result == null)
                return Unauthorized("Invalid credentials");


            return Ok(result);

        }




        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(service.GetAll());
        }




        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            var user = service.GetById(id);


            if (user == null)
                return NotFound();


            return Ok(user);

        }




        [HttpPut("{id}")]
        public IActionResult Update(int id, User user)
        {
            return Ok(service.Update(id, user));
        }




        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(service.Delete(id));
        }


    }

}