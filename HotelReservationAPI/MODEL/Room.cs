namespace HotelReservationAPI.MODEL
{
    public class Room
    {
        public int RoomID { get; set; }

        public string RoomNumber { get; set; }

        public string RoomType { get; set; }

        public decimal PricePerNight { get; set; }

        public int Capacity { get; set; }

        public string Status { get; set; }
    }
}
