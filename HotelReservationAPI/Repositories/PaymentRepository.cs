using HotelReservationAPI.Helper;
using HotelReservationAPI.MODEL;

using HotelReservationAPI.Repositories.Interfaces;
using Microsoft.Data.SqlClient;


namespace HotelReservationAPI.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {

        private readonly string conn = AppConfig.ConnectionString;



        public string AddPayment(Payment p)
        {
            try
            {
                using SqlConnection con = new SqlConnection(conn);


                string query = @"
                INSERT INTO Payments
                (ReservationID,Amount,PaymentDate,PaymentMethod,PaymentStatus)

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



                return "Success: Payment Added";

            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }

        }





        public List<Payment> GetAllPayments()
        {

            List<Payment> payments = new();


            using SqlConnection con = new SqlConnection(conn);


            string query = "SELECT * FROM Payments";


            SqlCommand cmd = new SqlCommand(query, con);



            con.Open();


            SqlDataReader reader = cmd.ExecuteReader();



            while (reader.Read())
            {

                payments.Add(new Payment
                {

                    PaymentID = (int)reader["PaymentID"],

                    ReservationID = (int)reader["ReservationID"],

                    Amount = (decimal)reader["Amount"],

                    PaymentDate = (DateTime)reader["PaymentDate"],

                    PaymentMethod = reader["PaymentMethod"].ToString(),

                    PaymentStatus = reader["PaymentStatus"].ToString()

                });

            }


            return payments;

        }






        public Payment GetPaymentById(int id)
        {

            Payment payment = null;


            using SqlConnection con = new SqlConnection(conn);


            string query =
            "SELECT * FROM Payments WHERE PaymentID=@ID";


            SqlCommand cmd = new SqlCommand(query, con);


            cmd.Parameters.AddWithValue("@ID", id);



            con.Open();


            SqlDataReader reader = cmd.ExecuteReader();



            if (reader.Read())
            {

                payment = new Payment
                {

                    PaymentID = (int)reader["PaymentID"],

                    ReservationID = (int)reader["ReservationID"],

                    Amount = (decimal)reader["Amount"],

                    PaymentDate = (DateTime)reader["PaymentDate"],

                    PaymentMethod = reader["PaymentMethod"].ToString(),

                    PaymentStatus = reader["PaymentStatus"].ToString()

                };

            }


            return payment;

        }






        public string UpdatePayment(Payment p)
        {

            using SqlConnection con = new SqlConnection(conn);


            string query = @"

            UPDATE Payments SET

            ReservationID=@ReservationID,

            Amount=@Amount,

            PaymentDate=@PaymentDate,

            PaymentMethod=@PaymentMethod,

            PaymentStatus=@PaymentStatus


            WHERE PaymentID=@PaymentID";



            SqlCommand cmd = new SqlCommand(query, con);



            cmd.Parameters.AddWithValue("@ReservationID", p.ReservationID);

            cmd.Parameters.AddWithValue("@Amount", p.Amount);

            cmd.Parameters.AddWithValue("@PaymentDate", p.PaymentDate);

            cmd.Parameters.AddWithValue("@PaymentMethod", p.PaymentMethod);

            cmd.Parameters.AddWithValue("@PaymentStatus", p.PaymentStatus);

            cmd.Parameters.AddWithValue("@PaymentID", p.PaymentID);



            con.Open();


            int rows = cmd.ExecuteNonQuery();



            return rows > 0 ?

            "Success: Payment Updated" :

            "Error: Payment Not Found";

        }





        public string DeletePayment(int id)
        {

            using SqlConnection con = new SqlConnection(conn);



            string query =
            "DELETE FROM Payments WHERE PaymentID=@ID";



            SqlCommand cmd = new SqlCommand(query, con);



            cmd.Parameters.AddWithValue("@ID", id);



            con.Open();



            int rows = cmd.ExecuteNonQuery();



            return rows > 0 ?

            "Success: Payment Deleted" :

            "Error: Payment Not Found";

        }

    }
}