using HotelReservationAPI.MODEL;
using Microsoft.Data.SqlClient;

namespace HotelReservationAPI.Helper
{
    public class CustomerHelper
    {
        private readonly string conn = "Data Source=DESKTOP-0ID2UPP;Initial Catalog=HotelReservationDB;Integrated Security=True;Trust Server Certificate=True";

        // ===================== ADD =====================
        public string AddCustomer(Customer customer)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    string query = @"
                        INSERT INTO Customers (FirstName, LastName, Phone, Email, Address, CreatedAt)
                        VALUES (@FirstName, @LastName, @Phone, @Email, @Address, @CreatedAt);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                        cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                        cmd.Parameters.AddWithValue("@Email", customer.Email);
                        cmd.Parameters.AddWithValue("@Address", customer.Address);
                        cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                        connection.Open();
                        int newId = (int)cmd.ExecuteScalar();
                        return $"Success: New CustomerID = {newId}";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        // ===================== GET ALL =====================
        public (List<Customer>, string) GetAllCustomers()
        {
            List<Customer> list = new List<Customer>();
            try
            {
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
                return (list, "Success");
            }
            catch (Exception ex)
            {
                return (list, $"Error: {ex.Message}");
            }
        }

        // ===================== GET BY ID =====================
        public (Customer?, string) GetCustomerById(int id)
        {
            Customer? customer = null;
            try
            {
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
                return (customer, "Success");
            }
            catch (Exception ex)
            {
                return (customer, $"Error: {ex.Message}");
            }
        }

        // ===================== UPDATE (Partial Update) =====================
        public string UpdateCustomer(Customer customer)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    List<string> updates = new List<string>();
                    SqlCommand cmd = new SqlCommand();

                    if (!string.IsNullOrEmpty(customer.FirstName))
                    {
                        updates.Add("FirstName = @FirstName");
                        cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    }
                    if (!string.IsNullOrEmpty(customer.LastName))
                    {
                        updates.Add("LastName = @LastName");
                        cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                    }
                    if (!string.IsNullOrEmpty(customer.Phone))
                    {
                        updates.Add("Phone = @Phone");
                        cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                    }
                    if (!string.IsNullOrEmpty(customer.Email))
                    {
                        updates.Add("Email = @Email");
                        cmd.Parameters.AddWithValue("@Email", customer.Email);
                    }
                    if (!string.IsNullOrEmpty(customer.Address))
                    {
                        updates.Add("Address = @Address");
                        cmd.Parameters.AddWithValue("@Address", customer.Address);
                    }

                    if (updates.Count == 0)
                        return "Error: No fields to update";

                    string query = $"UPDATE Customers SET {string.Join(", ", updates)} WHERE CustomerID = @CustomerID";
                    cmd.CommandText = query;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerID);

                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();

                    return rows > 0 ? "Success: Customer updated" : "Error: No rows affected";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        // ===================== DELETE =====================
        public string DeleteCustomer(int id)
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
                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0 ? "Success: Customer deleted" : "Error: No rows affected";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}
