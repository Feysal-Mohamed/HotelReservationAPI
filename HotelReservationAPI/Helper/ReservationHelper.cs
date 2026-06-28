using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using HotelReservationAPI.MODEL;

namespace HotelReservationAPI.Helper
{
    public class ReservationHelper
    {
        string conn = "Data Source=DESKTOP-0ID2UPP;Initial Catalog=HotelReservationDB;Integrated Security=True;Trust Server Certificate=True";

        // ================= ADD =================
        public string AddReservation(Reservation r)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    string query = @"INSERT INTO Reservations
                    (CustomerID, RoomID, CheckInDate, CheckOutDate, NumberOfGuests, ReservationStatus, TotalAmount)
                    VALUES
                    (@CustomerID, @RoomID, @CheckInDate, @CheckOutDate, @NumberOfGuests, @ReservationStatus, @TotalAmount)";

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

                    return "Reservation Added Successfully";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // ================= GET ALL =================
        public List<Reservation> GetAllReservations()
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

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // ================= GET BY ID =================
        public Reservation GetReservationById(int id)
        {
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
                        return new Reservation
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

                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // ================= UPDATE =================
        public string UpdateReservation(Reservation r)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    string query = @"UPDATE Reservations
                    SET CustomerID=@CustomerID,
                        RoomID=@RoomID,
                        CheckInDate=@CheckInDate,
                        CheckOutDate=@CheckOutDate,
                        NumberOfGuests=@NumberOfGuests,
                        ReservationStatus=@ReservationStatus,
                        TotalAmount=@TotalAmount
                    WHERE ReservationID=@ReservationID";

                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@ReservationID", r.ReservationID);
                    cmd.Parameters.AddWithValue("@CustomerID", r.CustomerID);
                    cmd.Parameters.AddWithValue("@RoomID", r.RoomID);
                    cmd.Parameters.AddWithValue("@CheckInDate", r.CheckInDate);
                    cmd.Parameters.AddWithValue("@CheckOutDate", r.CheckOutDate);
                    cmd.Parameters.AddWithValue("@NumberOfGuests", r.NumberOfGuests);
                    cmd.Parameters.AddWithValue("@ReservationStatus", r.ReservationStatus);
                    cmd.Parameters.AddWithValue("@TotalAmount", r.TotalAmount);

                    con.Open();

                    if (cmd.ExecuteNonQuery() > 0)
                        return "Reservation Updated Successfully";

                    return "Reservation Not Found";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
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

                    if (cmd.ExecuteNonQuery() > 0)
                        return "Reservation Deleted Successfully";

                    return "Reservation Not Found";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}