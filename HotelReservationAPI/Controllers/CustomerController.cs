using HotelReservationAPI.Helper;
using HotelReservationAPI.MODEL;
using Microsoft.AspNetCore.Mvc;
//using System.Linq;

namespace HotelReservationAPI.Controllers
{
    [ApiController]
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerHelper helper = new CustomerHelper();

        // ================= ADD =================
        [HttpPost]
        [HttpPost]
        public IActionResult Add(Customer customer)
        {
            // Get all customers
            var (customers, status) = helper.GetAllCustomers();

            if (status != "Success")
                return BadRequest(status);

            // Check if customer already exists
            bool exists = customers.Any(c =>
                c.Email.Equals(customer.Email, StringComparison.OrdinalIgnoreCase) ||
                c.Phone == customer.Phone);

            if (exists)
                return BadRequest("Customer already exists.");

            // Add customer
            string result = helper.AddCustomer(customer);

            if (result.StartsWith("Success"))
                return Ok(result);

            return BadRequest(result);
        }

        // ================= GET ALL =================
        [HttpGet]
        public IActionResult GetAll()
        {
            var (customers, status) = helper.GetAllCustomers();

            if (status == "Success")
                return Ok(customers);

            return BadRequest(status);
        }

        // ================= GET BY ID =================
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var (customer, status) = helper.GetCustomerById(id);

            if (status != "Success")
                return BadRequest(status);

            if (customer == null)
                return NotFound("Customer Not Found");

            return Ok(customer);
        }

        // ================= UPDATE =================
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Customer customer)
        {
            customer.CustomerID = id;
            string result = helper.UpdateCustomer(customer);

            if (result.StartsWith("Success"))
                return Ok(result);

            return BadRequest(result);
        }


        // ================= DELETE =================
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string result = helper.DeleteCustomer(id);

            if (result.StartsWith("Success"))
                return Ok(result);

            return BadRequest(result);
        }
    }
}
