using HotelReservationAPI.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            using var con = new SqlConnection(AppConfig.ConnectionString);
            con.Open();

            return Ok("Database connected successfully");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.ToString());
        }
    }
}