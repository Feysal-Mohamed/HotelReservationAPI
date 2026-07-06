using Microsoft.AspNetCore.Mvc;
using HotelReservationAPI.Helper;
using HotelReservationAPI.MODEL;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;

namespace HotelReservationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly userHelper _context;
        private readonly JwtHelper _jwtHelper;

        public UserController(userHelper context, JwtHelper jwtHelper)
        {
            _context = context;
            _jwtHelper = jwtHelper;
        }

        // ================= REGISTER =================
        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            if (await _context.Users.AnyAsync(u => u.Username == user.Username || u.Email == user.Email))
                return BadRequest("Username or Email already exists.");

            user.PasswordHash = HashPassword(user.PasswordHash);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully.");
        }

        // ================= LOGIN =================
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user == null || !VerifyPassword(request.PasswordHash, user.PasswordHash))
                return Unauthorized("Invalid credentials.");

            var token = _jwtHelper.GenerateToken(user.Username, user.Role);
            return Ok(new { Token = token });
        }

        // ================= GET ALL =================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        // ================= GET BY ID =================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound("User not found.");

            return Ok(user);
        }

        // ================= UPDATE =================
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User updatedUser)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound("User not found.");

            // Update only provided fields
            if (!string.IsNullOrEmpty(updatedUser.FullName))
                user.FullName = updatedUser.FullName;
            if (!string.IsNullOrEmpty(updatedUser.Username))
                user.Username = updatedUser.Username;
            if (!string.IsNullOrEmpty(updatedUser.Email))
                user.Email = updatedUser.Email;
            if (!string.IsNullOrEmpty(updatedUser.Role))
                user.Role = updatedUser.Role;
            if (!string.IsNullOrEmpty(updatedUser.PasswordHash))
                user.PasswordHash = HashPassword(updatedUser.PasswordHash);

            await _context.SaveChangesAsync();
            return Ok("User updated successfully.");
        }

        // ================= DELETE =================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound("User not found.");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok("User deleted successfully.");
        }

        // ================= PASSWORD HELPERS =================
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        private bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }
    }
}
