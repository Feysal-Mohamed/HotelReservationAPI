using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace HotelReservationAPI.MODEL
{
    public class RoomHelper
    {
        private readonly string conn = "Data Source=DESKTOP-0ID2UPP;Initial Catalog=HotelReservationDB;Integrated Security=True;Trust Server Certificate=True";

        // ================= ADD =================
        public string AddRoom(Room room)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    string query = @"
                        INSERT INTO Rooms (RoomNumber, RoomType, PricePerNight, Capacity, Status)
                        VALUES (@RoomNumber, @RoomType, @PricePerNight, @Capacity, @Status);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
                    cmd.Parameters.AddWithValue("@RoomType", room.RoomType);
                    cmd.Parameters.AddWithValue("@PricePerNight", room.PricePerNight);
                    cmd.Parameters.AddWithValue("@Capacity", room.Capacity);
                    cmd.Parameters.AddWithValue("@Status", room.Status);

                    con.Open();
                    int newId = (int)cmd.ExecuteScalar();

                    return $"Success: New RoomID = {newId}";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        // ================= GET ALL =================
        public (List<Room>, string) GetAllRooms()
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
                return (list, "Success");
            }
            catch (Exception ex)
            {
                return (list, $"Error: {ex.Message}");
            }
        }

        // ================= GET BY ID =================
        public (Room?, string) GetRoomById(int id)
        {
            Room? room = null;
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
                        room = new Room
                        {
                            RoomID = Convert.ToInt32(reader["RoomID"]),
                            RoomNumber = reader["RoomNumber"].ToString(),
                            RoomType = reader["RoomType"].ToString(),
                            PricePerNight = Convert.ToDecimal(reader["PricePerNight"]),
                            Capacity = Convert.ToInt32(reader["Capacity"]),
                            Status = reader["Status"].ToString()
                        };
                    }
                }
                return (room, "Success");
            }
            catch (Exception ex)
            {
                return (room, $"Error: {ex.Message}");
            }
        }

        // ================= UPDATE (Partial Update) =================
        public string UpdateRoom(Room room)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    List<string> updates = new List<string>();
                    SqlCommand cmd = new SqlCommand();

                    if (!string.IsNullOrEmpty(room.RoomNumber))
                    {
                        updates.Add("RoomNumber=@RoomNumber");
                        cmd.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
                    }
                    if (!string.IsNullOrEmpty(room.RoomType))
                    {
                        updates.Add("RoomType=@RoomType");
                        cmd.Parameters.AddWithValue("@RoomType", room.RoomType);
                    }
                    if (room.PricePerNight > 0)
                    {
                        updates.Add("PricePerNight=@PricePerNight");
                        cmd.Parameters.AddWithValue("@PricePerNight", room.PricePerNight);
                    }
                    if (room.Capacity > 0)
                    {
                        updates.Add("Capacity=@Capacity");
                        cmd.Parameters.AddWithValue("@Capacity", room.Capacity);
                    }
                    if (!string.IsNullOrEmpty(room.Status))
                    {
                        updates.Add("Status=@Status");
                        cmd.Parameters.AddWithValue("@Status", room.Status);
                    }

                    if (updates.Count == 0)
                        return "Error: No fields to update";

                    string query = $"UPDATE Rooms SET {string.Join(", ", updates)} WHERE RoomID=@RoomID";
                    cmd.CommandText = query;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@RoomID", room.RoomID);

                    con.Open();
                    int rows = cmd.ExecuteNonQuery();

                    return rows > 0 ? "Success: Room updated" : "Error: No rows affected";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
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
                    int rows = cmd.ExecuteNonQuery();

                    return rows > 0 ? "Success: Room deleted" : "Error: Room Not Found";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}
