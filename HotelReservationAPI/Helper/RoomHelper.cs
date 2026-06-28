using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace HotelReservationAPI.MODEL
{
    public class RoomHelper
    {
        string conn = "Data Source=DESKTOP-0ID2UPP;Initial Catalog=HotelReservationDB;Integrated Security=True;Trust Server Certificate=True";

        // ================= ADD =================
        public string AddRoom(Room room)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    string query = @"INSERT INTO Rooms
                                    (RoomNumber, RoomType, PricePerNight, Capacity, Status)
                                    VALUES
                                    (@RoomNumber, @RoomType, @PricePerNight, @Capacity, @Status)";

                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
                    cmd.Parameters.AddWithValue("@RoomType", room.RoomType);
                    cmd.Parameters.AddWithValue("@PricePerNight", room.PricePerNight);
                    cmd.Parameters.AddWithValue("@Capacity", room.Capacity);
                    cmd.Parameters.AddWithValue("@Status", room.Status);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    return "Room Added Successfully";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // ================= GET ALL =================
        public List<Room> GetAllRooms()
        {
            List<Room> list = new List<Room>();

            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    string query = "SELECT * FROM Rooms";

                    SqlCommand cmd = new SqlCommand(query, con);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new Room
                        {
                            RoomID = Convert.ToInt32(reader["RoomID"]),
                            RoomNumber = reader["RoomNumber"].ToString(),
                            RoomType = reader["RoomType"].ToString(),
                            PricePerNight = Convert.ToDecimal(reader["PricePerNight"]),
                            Capacity = Convert.ToInt32(reader["Capacity"]),
                            Status = reader["Status"].ToString()
                        });
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // ================= GET BY ID =================
        public Room GetRoomById(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    string query = "SELECT * FROM Rooms WHERE RoomID=@RoomID";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@RoomID", id);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        return new Room
                        {
                            RoomID = Convert.ToInt32(reader["RoomID"]),
                            RoomNumber = reader["RoomNumber"].ToString(),
                            RoomType = reader["RoomType"].ToString(),
                            PricePerNight = Convert.ToDecimal(reader["PricePerNight"]),
                            Capacity = Convert.ToInt32(reader["Capacity"]),
                            Status = reader["Status"].ToString()
                        };
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // ================= UPDATE =================
        public string UpdateRoom(Room room)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    string query = @"UPDATE Rooms
                                    SET RoomNumber=@RoomNumber,
                                        RoomType=@RoomType,
                                        PricePerNight=@PricePerNight,
                                        Capacity=@Capacity,
                                        Status=@Status
                                    WHERE RoomID=@RoomID";

                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@RoomID", room.RoomID);
                    cmd.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
                    cmd.Parameters.AddWithValue("@RoomType", room.RoomType);
                    cmd.Parameters.AddWithValue("@PricePerNight", room.PricePerNight);
                    cmd.Parameters.AddWithValue("@Capacity", room.Capacity);
                    cmd.Parameters.AddWithValue("@Status", room.Status);

                    con.Open();

                    if (cmd.ExecuteNonQuery() > 0)
                        return "Room Updated Successfully";

                    return "Room Not Found";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // ================= DELETE =================
        public string DeleteRoom(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    string query = "DELETE FROM Rooms WHERE RoomID=@RoomID";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@RoomID", id);

                    con.Open();

                    if (cmd.ExecuteNonQuery() > 0)
                        return "Room Deleted Successfully";

                    return "Room Not Found";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}