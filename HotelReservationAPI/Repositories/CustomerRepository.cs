using HotelReservationAPI.Helper;
using HotelReservationAPI.MODEL;
using HotelReservationAPI.Repositories.Interfaces;
using Microsoft.Data.SqlClient;


namespace HotelReservationAPI.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly string conn = AppConfig.ConnectionString;



        public string AddCustomer(Customer customer)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(conn);


                string query = @"
                INSERT INTO Customers
                (FirstName,LastName,Phone,Email,Address,CreatedAt)
                VALUES
                (@FirstName,@LastName,@Phone,@Email,@Address,@CreatedAt)";


                SqlCommand cmd = new SqlCommand(query, connection);


                cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@Address", customer.Address);
                cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);


                connection.Open();

                cmd.ExecuteNonQuery();


                return "Success: Customer Added";

            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }

        }



        public List<Customer> GetAllCustomers()
        {

            List<Customer> customers = new();


            using SqlConnection connection = new SqlConnection(conn);


            string query = "SELECT * FROM Customers";


            SqlCommand cmd = new SqlCommand(query, connection);


            connection.Open();


            SqlDataReader reader = cmd.ExecuteReader();



            while (reader.Read())
            {

                customers.Add(new Customer
                {

                    CustomerID = (int)reader["CustomerID"],

                    FirstName = reader["FirstName"].ToString(),

                    LastName = reader["LastName"].ToString(),

                    Phone = reader["Phone"].ToString(),

                    Email = reader["Email"].ToString(),

                    Address = reader["Address"].ToString(),

                    CreatedAt = (DateTime)reader["CreatedAt"]

                });

            }


            return customers;
        }





        public Customer GetCustomerById(int id)
        {

            Customer customer = null;


            using SqlConnection connection = new SqlConnection(conn);



            string query =
            "SELECT * FROM Customers WHERE CustomerID=@ID";


            SqlCommand cmd = new SqlCommand(query, connection);


            cmd.Parameters.AddWithValue("@ID", id);



            connection.Open();


            SqlDataReader reader = cmd.ExecuteReader();



            if (reader.Read())
            {

                customer = new Customer
                {

                    CustomerID = (int)reader["CustomerID"],

                    FirstName = reader["FirstName"].ToString(),

                    LastName = reader["LastName"].ToString(),

                    Phone = reader["Phone"].ToString(),

                    Email = reader["Email"].ToString(),

                    Address = reader["Address"].ToString(),

                    CreatedAt = (DateTime)reader["CreatedAt"]

                };

            }


            return customer;

        }





        public string UpdateCustomer(Customer customer)
        {

            using SqlConnection connection = new SqlConnection(conn);



            string query = @"

            UPDATE Customers SET

            FirstName=@FirstName,
            LastName=@LastName,
            Phone=@Phone,
            Email=@Email,
            Address=@Address

            WHERE CustomerID=@ID";



            SqlCommand cmd = new SqlCommand(query, connection);


            cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
            cmd.Parameters.AddWithValue("@LastName", customer.LastName);
            cmd.Parameters.AddWithValue("@Phone", customer.Phone);
            cmd.Parameters.AddWithValue("@Email", customer.Email);
            cmd.Parameters.AddWithValue("@Address", customer.Address);
            cmd.Parameters.AddWithValue("@ID", customer.CustomerID);



            connection.Open();


            int rows = cmd.ExecuteNonQuery();



            return rows > 0 ?
            "Success: Customer Updated" :
            "Error: Customer Not Found";

        }





        public string DeleteCustomer(int id)
        {

            using SqlConnection connection = new SqlConnection(conn);



            string query =
            "DELETE FROM Customers WHERE CustomerID=@ID";



            SqlCommand cmd = new SqlCommand(query, connection);


            cmd.Parameters.AddWithValue("@ID", id);



            connection.Open();


            int rows = cmd.ExecuteNonQuery();



            return rows > 0 ?
            "Success: Customer Deleted" :
            "Error: Customer Not Found";

        }

    }
}