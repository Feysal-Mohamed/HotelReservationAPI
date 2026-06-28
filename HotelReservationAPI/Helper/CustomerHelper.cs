using HotelReservationAPI.MODEL;

using Microsoft.Data.SqlClient;

namespace HotelReservationAPI.Helper
{
    public class CustomerHelper
    {

        private readonly string conn = "Data Source=DESKTOP-0ID2UPP;Initial Catalog=HotelReservationDB;Integrated Security=True;Trust Server Certificate=True";

        // ===================== ADD =====================
        public bool AddCustomer(Customer customer)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    string query = @"
                    INSERT INTO Customers
                    (FirstName, LastName, Phone, Email, Address, CreatedAt)
                        VALUES
                        (@FirstName, @LastName, @Phone, @Email, @Address, @CreatedAt)";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                        cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                        cmd.Parameters.AddWithValue("@Email", customer.Email);
                        cmd.Parameters.AddWithValue("@Address", customer.Address);
                        cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                        connection.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        // ===================== GET ALL =====================
        public List<Customer> GetAllCustomers()
        {
            List<Customer> list = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(conn))
            {
                string query = "SELECT * FROM Customers";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new Customer
                        {
                            CustomerID = Convert.ToInt32(reader["CustomerID"]),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Email = reader["Email"].ToString(),
                            Address = reader["Address"].ToString(),
                            CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                        });
                    }
                }
            }

            return list;
        }

        // ===================== GET BY ID =====================
        public Customer GetCustomerById(int id)
        {
            Customer customer = null;

            using (SqlConnection connection = new SqlConnection(conn))
            {
                string query = "SELECT * FROM Customers WHERE CustomerID = @Id";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        customer = new Customer
                        {
                            CustomerID = Convert.ToInt32(reader["CustomerID"]),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Email = reader["Email"].ToString(),
                            Address = reader["Address"].ToString(),
                            CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                        };
                    }
                }
            }

            return customer;
        }

        // ===================== UPDATE (PUT) =====================
        public bool UpdateCustomer(Customer customer)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    string query = @"
                        UPDATE Customers
                        SET FirstName = @FirstName,
                            LastName = @LastName,
                            Phone = @Phone,
                            Email = @Email,
                            Address = @Address
                        WHERE CustomerID = @CustomerID";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                        cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                        cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                        cmd.Parameters.AddWithValue("@Email", customer.Email);
                        cmd.Parameters.AddWithValue("@Address", customer.Address);

                        connection.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        // ===================== DELETE =====================
        public bool DeleteCustomer(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    string query = "DELETE FROM Customers WHERE CustomerID = @Id";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);

                        connection.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}


