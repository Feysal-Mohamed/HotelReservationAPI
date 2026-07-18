using System.ComponentModel.DataAnnotations;

namespace HotelReservationAPI.MODEL
{
    public class User
    {
        public int UserID { get; set; }

        public string FullName { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        // NEW: Default role is User
        public string Role { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}