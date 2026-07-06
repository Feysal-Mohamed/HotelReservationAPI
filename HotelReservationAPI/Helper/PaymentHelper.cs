using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace HotelReservationAPI.MODEL
{
    public class PaymentHelper
    {
        private readonly string conn = "Data Source=DESKTOP-0ID2UPP;Initial Catalog=HotelReservationDB;Integrated Security=True;Trust Server Certificate=True";

        // ================= ADD =================
        public string AddPayment(Payment p)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    string query = @"
                        INSERT INTO Payments (ReservationID, Amount, PaymentDate, PaymentMethod, PaymentStatus)
                        VALUES (@ReservationID, @Amount, @PaymentDate, @PaymentMethod, @PaymentStatus);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ReservationID", p.ReservationID);
                    cmd.Parameters.AddWithValue("@Amount", p.Amount);
                    cmd.Parameters.AddWithValue("@PaymentDate", p.PaymentDate);
                    cmd.Parameters.AddWithValue("@PaymentMethod", p.PaymentMethod);
                    cmd.Parameters.AddWithValue("@PaymentStatus", p.PaymentStatus);

                    con.Open();
                    int newId = (int)cmd.ExecuteScalar();

                    return $"Success: New PaymentID = {newId}";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        // ================= GET ALL =================
        public (List<Payment>, string) GetAllPayments()
        {
            List<Payment> list = new List<Payment>();
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    string query = "SELECT * FROM Payments";
                    SqlCommand cmd = new SqlCommand(query, con);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new Payment
                        {
                            PaymentID = Convert.ToInt32(reader["PaymentID"]),
                            ReservationID = Convert.ToInt32(reader["ReservationID"]),
                            Amount = Convert.ToDecimal(reader["Amount"]),
                            PaymentDate = Convert.ToDateTime(reader["PaymentDate"]),
                            PaymentMethod = reader["PaymentMethod"].ToString(),
                            PaymentStatus = reader["PaymentStatus"].ToString()
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
        public (Payment?, string) GetPaymentById(int id)
        {
            Payment? payment = null;
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    string query = "SELECT * FROM Payments WHERE PaymentID=@ID";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        payment = new Payment
                        {
                            PaymentID = Convert.ToInt32(reader["PaymentID"]),
                            ReservationID = Convert.ToInt32(reader["ReservationID"]),
                            Amount = Convert.ToDecimal(reader["Amount"]),
                            PaymentDate = Convert.ToDateTime(reader["PaymentDate"]),
                            PaymentMethod = reader["PaymentMethod"].ToString(),
                            PaymentStatus = reader["PaymentStatus"].ToString()
                        };
                    }
                }
                return (payment, "Success");
            }
            catch (Exception ex)
            {
                return (payment, $"Error: {ex.Message}");
            }
        }

        // ================= UPDATE (Partial Update) =================
        public string UpdatePayment(Payment p)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    List<string> updates = new List<string>();
                    SqlCommand cmd = new SqlCommand();

                    if (p.ReservationID > 0)
                    {
                        updates.Add("ReservationID=@ReservationID");
                        cmd.Parameters.AddWithValue("@ReservationID", p.ReservationID);
                    }
                    if (p.Amount > 0)
                    {
                        updates.Add("Amount=@Amount");
                        cmd.Parameters.AddWithValue("@Amount", p.Amount);
                    }
                    if (p.PaymentDate != DateTime.MinValue)
                    {
                        updates.Add("PaymentDate=@PaymentDate");
                        cmd.Parameters.AddWithValue("@PaymentDate", p.PaymentDate);
                    }
                    if (!string.IsNullOrEmpty(p.PaymentMethod))
                    {
                        updates.Add("PaymentMethod=@PaymentMethod");
                        cmd.Parameters.AddWithValue("@PaymentMethod", p.PaymentMethod);
                    }
                    if (!string.IsNullOrEmpty(p.PaymentStatus))
                    {
                        updates.Add("PaymentStatus=@PaymentStatus");
                        cmd.Parameters.AddWithValue("@PaymentStatus", p.PaymentStatus);
                    }

                    if (updates.Count == 0)
                        return "Error: No fields to update";

                    string query = $"UPDATE Payments SET {string.Join(", ", updates)} WHERE PaymentID=@PaymentID";
                    cmd.CommandText = query;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@PaymentID", p.PaymentID);

                    con.Open();
                    int rows = cmd.ExecuteNonQuery();

                    return rows > 0 ? "Success: Payment updated" : "Error: No rows affected";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        // ================= DELETE =================
        public string DeletePayment(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    string query = "DELETE FROM Payments WHERE PaymentID=@ID";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();
                    int rows = cmd.ExecuteNonQuery();

                    return rows > 0 ? "Success: Payment deleted" : "Error: Payment Not Found";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}
