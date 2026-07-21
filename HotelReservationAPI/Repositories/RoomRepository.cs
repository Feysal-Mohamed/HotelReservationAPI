using HotelReservationAPI.MODEL;
using HotelReservationAPI.Repositories.Interfaces;
using HotelReservationAPI.Helper;
using Microsoft.Data.SqlClient;

namespace HotelReservationAPI.Repositories
{
    public class RoomRepository : IRoomRepository
    {

        private readonly string conn = AppConfig.ConnectionString;


        public string AddRoom(Room room)
        {
            try
            {
                using SqlConnection con = new SqlConnection(conn);

                string query = @"
                INSERT INTO Rooms
                (RoomNumber,RoomType,PricePerNight,Capacity,Status)
                VALUES
                (@RoomNumber,@RoomType,@PricePerNight,@Capacity,@Status)";


                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
                cmd.Parameters.AddWithValue("@RoomType", room.RoomType);
                cmd.Parameters.AddWithValue("@PricePerNight", room.PricePerNight);
                cmd.Parameters.AddWithValue("@Capacity", room.Capacity);
                cmd.Parameters.AddWithValue("@Status", room.Status);


                con.Open();
                cmd.ExecuteNonQuery();


                return "Success: Room Added";

            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }



        public List<Room> GetAllRooms()
        {

            List<Room> rooms = new();


            using SqlConnection con = new SqlConnection(conn);


            string query = "SELECT * FROM Rooms";


            SqlCommand cmd = new SqlCommand(query, con);


            con.Open();


            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {

                rooms.Add(new Room
                {
                    RoomID = (int)reader["RoomID"],
                    RoomNumber = reader["RoomNumber"].ToString(),
                    RoomType = reader["RoomType"].ToString(),
                    PricePerNight = (decimal)reader["PricePerNight"],
                    Capacity = (int)reader["Capacity"],
                    Status = reader["Status"].ToString()
                });

            }


            return rooms;
        }



        public Room GetRoomById(int id)
        {

            using SqlConnection con = new SqlConnection(conn);


            string query =
            "SELECT * FROM Rooms WHERE RoomID=@id";


            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@id", id);


            con.Open();


            SqlDataReader reader = cmd.ExecuteReader();


            if (reader.Read())
            {
                return new Room
                {
                    RoomID = (int)reader["RoomID"],
                    RoomNumber = reader["RoomNumber"].ToString(),
                    RoomType = reader["RoomType"].ToString(),
                    PricePerNight = (decimal)reader["PricePerNight"],
                    Capacity = (int)reader["Capacity"],
                    Status = reader["Status"].ToString()
                };
            }


            return null;
        }



        public string UpdateRoom(Room room)
        {
            using SqlConnection con = new SqlConnection(conn);


            string query = @"
            UPDATE Rooms SET
            RoomNumber=@RoomNumber,
            RoomType=@RoomType,
            PricePerNight=@Price,
            Capacity=@Capacity,
            Status=@Status

            WHERE RoomID=@ID";


            SqlCommand cmd = new SqlCommand(query, con);


            cmd.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
            cmd.Parameters.AddWithValue("@RoomType", room.RoomType);
            cmd.Parameters.AddWithValue("@Price", room.PricePerNight);
            cmd.Parameters.AddWithValue("@Capacity", room.Capacity);
            cmd.Parameters.AddWithValue("@Status", room.Status);
            cmd.Parameters.AddWithValue("@ID", room.RoomID);


            con.Open();

            int rows = cmd.ExecuteNonQuery();


            return rows > 0 ?
            "Success: Updated" :
            "Error: Not found";
        }



        public string DeleteRoom(int id)
        {

            using SqlConnection con = new SqlConnection(conn);


            string query =
            "DELETE FROM Rooms WHERE RoomID=@id";


            SqlCommand cmd = new SqlCommand(query, con);


            cmd.Parameters.AddWithValue("@id", id);


            con.Open();


            int rows = cmd.ExecuteNonQuery();


            return rows > 0 ?
            "Success: Deleted" :
            "Error: Not Found";

        }
    }
}