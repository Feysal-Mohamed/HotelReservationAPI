using HotelReservationAPI.Helper;
using HotelReservationAPI.MODEL;
using HotelReservationAPI.Repositories.Interfaces;
using Microsoft.Data.SqlClient;


namespace HotelReservationAPI.Repositories
{
    public class ReservationRepository : IReservationRepository
    {

        private readonly string conn = AppConfig.ConnectionString;



        public string AddReservation(Reservation r)
        {
            try
            {
                using SqlConnection con = new SqlConnection(conn);


                string query = @"
                INSERT INTO Reservations
                (
                CustomerID,
                RoomID,
                CheckInDate,
                CheckOutDate,
                NumberOfGuests,
                ReservationStatus,
                TotalAmount
                )

                VALUES

                (
                @CustomerID,
                @RoomID,
                @CheckInDate,
                @CheckOutDate,
                @NumberOfGuests,
                @ReservationStatus,
                @TotalAmount
                )";


                SqlCommand cmd = new SqlCommand(query, con);


                cmd.Parameters.AddWithValue("@CustomerID", r.CustomerID);
                cmd.Parameters.AddWithValue("@RoomID", r.RoomID);
                cmd.Parameters.AddWithValue("@CheckInDate", r.CheckInDate);
                cmd.Parameters.AddWithValue("@CheckOutDate", r.CheckOutDate);
                cmd.Parameters.AddWithValue("@NumberOfGuests", r.NumberOfGuests);
                cmd.Parameters.AddWithValue("@ReservationStatus", r.ReservationStatus);
                cmd.Parameters.AddWithValue("@TotalAmount", r.TotalAmount);


                con.Open();

                cmd.ExecuteNonQuery();


                return "Success: Reservation Added";

            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }

        }




        public List<Reservation> GetAllReservations()
        {

            List<Reservation> reservations = new();


            using SqlConnection con = new SqlConnection(conn);


            string query = "SELECT * FROM Reservations";


            SqlCommand cmd = new SqlCommand(query, con);


            con.Open();


            SqlDataReader reader = cmd.ExecuteReader();



            while (reader.Read())
            {

                reservations.Add(new Reservation
                {

                    ReservationID = (int)reader["ReservationID"],

                    CustomerID = (int)reader["CustomerID"],

                    RoomID = (int)reader["RoomID"],

                    CheckInDate = (DateTime)reader["CheckInDate"],

                    CheckOutDate = (DateTime)reader["CheckOutDate"],

                    NumberOfGuests = (int)reader["NumberOfGuests"],

                    ReservationStatus = reader["ReservationStatus"].ToString(),

                    TotalAmount = (decimal)reader["TotalAmount"]

                });

            }


            return reservations;

        }





        public Reservation GetReservationById(int id)
        {

            Reservation reservation = null;


            using SqlConnection con = new SqlConnection(conn);



            string query =
            "SELECT * FROM Reservations WHERE ReservationID=@ID";


            SqlCommand cmd = new SqlCommand(query, con);


            cmd.Parameters.AddWithValue("@ID", id);



            con.Open();


            SqlDataReader reader = cmd.ExecuteReader();



            if (reader.Read())
            {

                reservation = new Reservation
                {

                    ReservationID = (int)reader["ReservationID"],

                    CustomerID = (int)reader["CustomerID"],

                    RoomID = (int)reader["RoomID"],

                    CheckInDate = (DateTime)reader["CheckInDate"],

                    CheckOutDate = (DateTime)reader["CheckOutDate"],

                    NumberOfGuests = (int)reader["NumberOfGuests"],

                    ReservationStatus = reader["ReservationStatus"].ToString(),

                    TotalAmount = (decimal)reader["TotalAmount"]

                };

            }


            return reservation;

        }





        public string UpdateReservation(Reservation r)
        {

            using SqlConnection con = new SqlConnection(conn);


            string query = @"

            UPDATE Reservations SET

            CustomerID=@CustomerID,

            RoomID=@RoomID,

            CheckInDate=@CheckInDate,

            CheckOutDate=@CheckOutDate,

            NumberOfGuests=@NumberOfGuests,

            ReservationStatus=@ReservationStatus,

            TotalAmount=@TotalAmount


            WHERE ReservationID=@ID";



            SqlCommand cmd = new SqlCommand(query, con);



            cmd.Parameters.AddWithValue("@CustomerID", r.CustomerID);
            cmd.Parameters.AddWithValue("@RoomID", r.RoomID);
            cmd.Parameters.AddWithValue("@CheckInDate", r.CheckInDate);
            cmd.Parameters.AddWithValue("@CheckOutDate", r.CheckOutDate);
            cmd.Parameters.AddWithValue("@NumberOfGuests", r.NumberOfGuests);
            cmd.Parameters.AddWithValue("@ReservationStatus", r.ReservationStatus);
            cmd.Parameters.AddWithValue("@TotalAmount", r.TotalAmount);
            cmd.Parameters.AddWithValue("@ID", r.ReservationID);



            con.Open();


            int rows = cmd.ExecuteNonQuery();



            return rows > 0 ?
            "Success: Reservation Updated" :
            "Error: Reservation Not Found";

        }





        public string DeleteReservation(int id)
        {

            using SqlConnection con = new SqlConnection(conn);


            string query =
            "DELETE FROM Reservations WHERE ReservationID=@ID";


            SqlCommand cmd = new SqlCommand(query, con);


            cmd.Parameters.AddWithValue("@ID", id);


            con.Open();


            int rows = cmd.ExecuteNonQuery();


            return rows > 0 ?
            "Success: Reservation Deleted" :
            "Error: Reservation Not Found";

        }

    }
}