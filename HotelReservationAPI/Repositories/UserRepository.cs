using HotelReservationAPI.Helper;
using HotelReservationAPI.MODEL;
using HotelReservationAPI.Repositories.Interfaces;
using Microsoft.Data.SqlClient;


namespace HotelReservationAPI.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly string conn = AppConfig.ConnectionString;



        public bool CheckUserExists(string username, string email)
        {
            using SqlConnection con = new SqlConnection(conn);


            string query = @"
            SELECT COUNT(*)
            FROM Users
            WHERE Username=@Username
            OR Email=@Email";


            SqlCommand cmd = new SqlCommand(query, con);


            cmd.Parameters.AddWithValue("@Username", username);

            cmd.Parameters.AddWithValue("@Email", email);



            con.Open();


            int count = (int)cmd.ExecuteScalar();


            return count > 0;
        }





        public string AddUser(User user)
        {

            try
            {

                using SqlConnection con = new SqlConnection(conn);


                string query = @"

                INSERT INTO Users
                (
                FullName,
                Username,
                Email,
                PasswordHash,
                Role,
                CreatedAt
                )

                VALUES

                (
                @FullName,
                @Username,
                @Email,
                @PasswordHash,
                @Role,
                @CreatedAt
                )";



                SqlCommand cmd = new SqlCommand(query, con);


                cmd.Parameters.AddWithValue("@FullName", user.FullName);

                cmd.Parameters.AddWithValue("@Username", user.Username);

                cmd.Parameters.AddWithValue("@Email", user.Email);

                cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);

                cmd.Parameters.AddWithValue("@Role", user.Role);

                cmd.Parameters.AddWithValue("@CreatedAt", user.CreatedAt);



                con.Open();


                cmd.ExecuteNonQuery();



                return "Success: User Registered";

            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }

        }





        public List<User> GetAllUsers()
        {

            List<User> users = new();



            using SqlConnection con = new SqlConnection(conn);



            string query = "SELECT * FROM Users";



            SqlCommand cmd = new SqlCommand(query, con);



            con.Open();



            SqlDataReader reader = cmd.ExecuteReader();



            while (reader.Read())
            {

                users.Add(new User
                {

                    UserID = (int)reader["UserID"],

                    FullName = reader["FullName"].ToString(),

                    Username = reader["Username"].ToString(),

                    Email = reader["Email"].ToString(),

                    PasswordHash = reader["PasswordHash"].ToString(),

                    Role = reader["Role"].ToString(),

                    CreatedAt = (DateTime)reader["CreatedAt"]

                });

            }


            return users;

        }






        public User GetUserByEmail(string email)
        {

            User user = null;


            using SqlConnection con = new SqlConnection(conn);



            string query =
            "SELECT * FROM Users WHERE Email=@Email";



            SqlCommand cmd = new SqlCommand(query, con);



            cmd.Parameters.AddWithValue("@Email", email);



            con.Open();



            SqlDataReader reader = cmd.ExecuteReader();



            if (reader.Read())
            {

                user = new User
                {

                    UserID = (int)reader["UserID"],

                    FullName = reader["FullName"].ToString(),

                    Username = reader["Username"].ToString(),

                    Email = reader["Email"].ToString(),

                    PasswordHash = reader["PasswordHash"].ToString(),

                    Role = reader["Role"].ToString(),

                    CreatedAt = (DateTime)reader["CreatedAt"]

                };

            }


            return user;

        }





        public User GetUserById(int id)
        {

            User user = null;


            using SqlConnection con = new SqlConnection(conn);


            string query =
            "SELECT * FROM Users WHERE UserID=@ID";



            SqlCommand cmd = new SqlCommand(query, con);


            cmd.Parameters.AddWithValue("@ID", id);



            con.Open();



            SqlDataReader reader = cmd.ExecuteReader();



            if (reader.Read())
            {

                user = new User
                {

                    UserID = (int)reader["UserID"],

                    FullName = reader["FullName"].ToString(),

                    Username = reader["Username"].ToString(),

                    Email = reader["Email"].ToString(),

                    PasswordHash = reader["PasswordHash"].ToString(),

                    Role = reader["Role"].ToString()

                };

            }


            return user;

        }






        public string UpdateUser(User user)
        {

            using SqlConnection con = new SqlConnection(conn);



            string query = @"

            UPDATE Users SET

            FullName=@FullName,

            Username=@Username,

            Email=@Email,

            PasswordHash=@PasswordHash,

            Role=@Role


            WHERE UserID=@ID";



            SqlCommand cmd = new SqlCommand(query, con);



            cmd.Parameters.AddWithValue("@FullName", user.FullName);

            cmd.Parameters.AddWithValue("@Username", user.Username);

            cmd.Parameters.AddWithValue("@Email", user.Email);

            cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);

            cmd.Parameters.AddWithValue("@Role", user.Role);

            cmd.Parameters.AddWithValue("@ID", user.UserID);



            con.Open();


            int rows = cmd.ExecuteNonQuery();



            return rows > 0 ?
            "Success: User Updated" :
            "Error: User Not Found";

        }






        public string DeleteUser(int id)
        {

            using SqlConnection con = new SqlConnection(conn);


            string query =
            "DELETE FROM Users WHERE UserID=@ID";



            SqlCommand cmd = new SqlCommand(query, con);


            cmd.Parameters.AddWithValue("@ID", id);



            con.Open();


            int rows = cmd.ExecuteNonQuery();



            return rows > 0 ?
            "Success: User Deleted" :
            "Error: User Not Found";

        }

    }
}