using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using HotelReservationAPI.MODEL;

namespace HotelReservationAPI.Helper
{
    public class ReservationHelper
    {
        private readonly string conn = "Data Source=DESKTOP-0ID2UPP;Initial Catalog=HotelReservationDB;Integrated Security=True;Trust Server Certificate=True";

        // ================= ADD =================
        public string AddReservation(Reservation r)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    string query = @"
                        INSERT INTO Reservations (CustomerID, RoomID, CheckInDate, CheckOutDate, NumberOfGuests, ReservationStatus, TotalAmount)
                        VALUES (@CustomerID, @RoomID, @CheckInDate, @CheckOutDate, @NumberOfGuests, @ReservationStatus, @TotalAmount);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@CustomerID", r.CustomerID);
                    cmd.Parameters.AddWithValue("@RoomID", r.RoomID);
                    cmd.Parameters.AddWithValue("@CheckInDate", r.CheckInDate);
                    cmd.Parameters.AddWithValue("@CheckOutDate", r.CheckOutDate);
                    cmd.Parameters.AddWithValue("@NumberOfGuests", r.NumberOfGuests);
                    cmd.Parameters.AddWithValue("@ReservationStatus", r.ReservationStatus);
                    cmd.Parameters.AddWithValue("@TotalAmount", r.TotalAmount);

                    con.Open();
                    int newId = (int)cmd.ExecuteScalar();

                    return $"Success: New ReservationID = {newId}";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        // ================= GET ALL =================
        public (List<Reservation>, string) GetAllReservations()
        {
            List<Reservation> list = new List<Reservation>();
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    string query = "SELECT * FROM Reservations";
                    SqlCommand cmd = new SqlCommand(query, con);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new Reservation
                        {
                            ReservationID = Convert.ToInt32(reader["ReservationID"]),
                            CustomerID = Convert.ToInt32(reader["CustomerID"]),
                            RoomID = Convert.ToInt32(reader["RoomID"]),
                            CheckInDate = Convert.ToDateTime(reader["CheckInDate"]),
                            CheckOutDate = Convert.ToDateTime(reader["CheckOutDate"]),
                            NumberOfGuests = Convert.ToInt32(reader["NumberOfGuests"]),
                            ReservationStatus = reader["ReservationStatus"].ToString(),
                            TotalAmount = Convert.ToDecimal(reader["TotalAmount"])
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
        public (Reservation?, string) GetReservationById(int id)
        {
            Reservation? reservation = null;
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    string query = "SELECT * FROM Reservations WHERE ReservationID=@ID";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        reservation = new Reservation
                        {
                            ReservationID = Convert.ToInt32(reader["ReservationID"]),
                            CustomerID = Convert.ToInt32(reader["CustomerID"]),
                            RoomID = Convert.ToInt32(reader["RoomID"]),
                            CheckInDate = Convert.ToDateTime(reader["CheckInDate"]),
                            CheckOutDate = Convert.ToDateTime(reader["CheckOutDate"]),
                            NumberOfGuests = Convert.ToInt32(reader["NumberOfGuests"]),
                            ReservationStatus = reader["ReservationStatus"].ToString(),
                            TotalAmount = Convert.ToDecimal(reader["TotalAmount"])
                        };
                    }
                }
                return (reservation, "Success");
            }
            catch (Exception ex)
            {
                return (reservation, $"Error: {ex.Message}");
            }
        }

        // ================= UPDATE (Partial Update) =================
        public string UpdateReservation(Reservation r)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    List<string> updates = new List<string>();
                    SqlCommand cmd = new SqlCommand();

                    if (r.CustomerID > 0)
                    {
                        updates.Add("CustomerID=@CustomerID");
                        cmd.Parameters.AddWithValue("@CustomerID", r.CustomerID);
                    }
                    if (r.RoomID > 0)
                    {
                        updates.Add("RoomID=@RoomID");
                        cmd.Parameters.AddWithValue("@RoomID", r.RoomID);
                    }
                    if (r.CheckInDate != DateTime.MinValue)
                    {
                        updates.Add("CheckInDate=@CheckInDate");
                        cmd.Parameters.AddWithValue("@CheckInDate", r.CheckInDate);
                    }
                    if (r.CheckOutDate != DateTime.MinValue)
                    {
                        updates.Add("CheckOutDate=@CheckOutDate");
                        cmd.Parameters.AddWithValue("@CheckOutDate", r.CheckOutDate);
                    }
                    if (r.NumberOfGuests > 0)
                    {
                        updates.Add("NumberOfGuests=@NumberOfGuests");
                        cmd.Parameters.AddWithValue("@NumberOfGuests", r.NumberOfGuests);
                    }
                    if (!string.IsNullOrEmpty(r.ReservationStatus))
                    {
                        updates.Add("ReservationStatus=@ReservationStatus");
                        cmd.Parameters.AddWithValue("@ReservationStatus", r.ReservationStatus);
                    }
                    if (r.TotalAmount > 0)
                    {
                        updates.Add("TotalAmount=@TotalAmount");
                        cmd.Parameters.AddWithValue("@TotalAmount", r.TotalAmount);
                    }

                    if (updates.Count == 0)
                        return "Error: No fields to update";

                    string query = $"UPDATE Reservations SET {string.Join(", ", updates)} WHERE ReservationID=@ReservationID";
                    cmd.CommandText = query;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@ReservationID", r.ReservationID);

                    con.Open();
                    int rows = cmd.ExecuteNonQuery();

                    return rows > 0 ? "Success: Reservation updated" : "Error: No rows affected";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        // ================= DELETE =================
        public string DeleteReservation(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    string query = "DELETE FROM Reservations WHERE ReservationID=@ID";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();
                    int rows = cmd.ExecuteNonQuery();

                    return rows > 0 ? "Success: Reservation deleted" : "Error: Reservation Not Found";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}
