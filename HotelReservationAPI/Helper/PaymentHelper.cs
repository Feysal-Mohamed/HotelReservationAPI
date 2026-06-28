using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace HotelReservationAPI.MODEL
{
    public class PaymentHelper
    {
        string conn = "Data Source=DESKTOP-0ID2UPP;Initial Catalog=HotelReservationDB;Integrated Security=True;Trust Server Certificate=True";

        // ================= ADD =================
        public string AddPayment(Payment p)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    string query = @"INSERT INTO Payments
                                    (ReservationID, Amount, PaymentDate, PaymentMethod, PaymentStatus)
                                    VALUES
                                    (@ReservationID,@Amount,@PaymentDate,@PaymentMethod,@PaymentStatus)";

                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@ReservationID", p.ReservationID);
                    cmd.Parameters.AddWithValue("@Amount", p.Amount);
                    cmd.Parameters.AddWithValue("@PaymentDate", p.PaymentDate);
                    cmd.Parameters.AddWithValue("@PaymentMethod", p.PaymentMethod);
                    cmd.Parameters.AddWithValue("@PaymentStatus", p.PaymentStatus);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    return "Payment Added Successfully";
                }
            }
            catch (Exception ex)
            {
                return ex.Message ;
            }
        }

        // ================= GET ALL =================
        public List<Payment> GetAllPayments()
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
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return list;
        }

        // ================= GET BY ID =================
        public Payment GetPaymentById(int id)
        {
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
                        return new Payment
                        {
                            PaymentID = Convert.ToInt32(reader["PaymentID"]),
                            ReservationID = Convert.ToInt32(reader["ReservationID"]),
                            Amount = Convert.ToDecimal(reader["Amount"]),
                            PaymentDate = Convert.ToDateTime(reader["PaymentDate"]),
                            PaymentMethod = reader["PaymentMethod"].ToString(),
                            PaymentStatus = reader["PaymentStatus"].ToString()
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
        public string UpdatePayment(Payment p)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    string query = @"UPDATE Payments
                                    SET ReservationID=@ReservationID,
                                        Amount=@Amount,
                                        PaymentDate=@PaymentDate,
                                        PaymentMethod=@PaymentMethod,
                                        PaymentStatus=@PaymentStatus
                                    WHERE PaymentID=@PaymentID";

                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@PaymentID", p.PaymentID);
                    cmd.Parameters.AddWithValue("@ReservationID", p.ReservationID);
                    cmd.Parameters.AddWithValue("@Amount", p.Amount);
                    cmd.Parameters.AddWithValue("@PaymentDate", p.PaymentDate);
                    cmd.Parameters.AddWithValue("@PaymentMethod", p.PaymentMethod);
                    cmd.Parameters.AddWithValue("@PaymentStatus", p.PaymentStatus);

                    con.Open();

                    if (cmd.ExecuteNonQuery() > 0)
                        return "Payment Updated Successfully";

                    return "Payment Not Found";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
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

                    if (cmd.ExecuteNonQuery() > 0)
                        return "Payment Deleted Successfully";

                    return "Payment Not Found";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}