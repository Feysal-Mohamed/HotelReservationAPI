using HotelReservationAPI.Helper;
using HotelReservationAPI.MODEL;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationAPI.Controllers
{
    [ApiController]
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        CustomerHelper helper = new CustomerHelper();

        // ================= ADD =================
        [HttpPost]
        public IActionResult Add(Customer customer)
        {
            return Ok(helper.AddCustomer(customer));
        }

        // ================= GET ALL =================
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(helper.GetAllCustomers());
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
                Customer customer = helper.GetCustomerById(id);

                if (customer == null)
                    return NotFound("Customer Not Found");

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ================= UPDATE =================
        [HttpPut("{id}")]
        public IActionResult Update(int id, Customer customer)
        {
            customer.CustomerID = id;
            return Ok(helper.UpdateCustomer(customer));
        }

        // ================= DELETE =================
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(helper.DeleteCustomer(id));
        }
    }
}