namespace HotelReservationAPI.MODEL
{
    public class Reservation
    {
        public int ReservationID { get; set; }

        public int CustomerID { get; set; }

        public int RoomID { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public int NumberOfGuests { get; set; }

        public string ReservationStatus { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
