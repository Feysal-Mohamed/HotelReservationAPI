using HotelReservationAPI.Helper;
using HotelReservationAPI.MODEL;
using HotelReservationAPI.Repositories.Interfaces;
using HotelReservationAPI.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;


namespace HotelReservationAPI.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository repository;

        private readonly JwtHelper jwt;


        public UserService(
            IUserRepository repository,
            JwtHelper jwt)
        {
            this.repository = repository;
            this.jwt = jwt;
        }





        public string Register(User user)
        {

            if (repository.CheckUserExists(user.Username, user.Email))
                return "Username or Email already exists";



            user.Role =
            string.IsNullOrWhiteSpace(user.Role)
            ? "User"
            : user.Role;



            user.PasswordHash =
            HashPassword(user.PasswordHash);



            return repository.AddUser(user);

        }






        public object Login(LoginRequest request)
        {

            User user =
            repository.GetUserByEmail(request.Email);



            if (user == null)
                return null;



            if (!VerifyPassword(
                request.PasswordHash,
                user.PasswordHash))
                return null;



            string token =
            jwt.GenerateToken(
                user.Email,
                user.Role);



            return new
            {
                Token = token,
                Role = user.Role,
                FullName = user.FullName
            };

        }





        public List<User> GetAll()
        {
            return repository.GetAllUsers();
        }





        public User GetById(int id)
        {
            return repository.GetUserById(id);
        }





        public string Update(int id, User user)
        {

            user.UserID = id;


            if (!string.IsNullOrEmpty(user.PasswordHash))
            {
                user.PasswordHash =
                HashPassword(user.PasswordHash);
            }


            return repository.UpdateUser(user);

        }





        public string Delete(int id)
        {
            return repository.DeleteUser(id);
        }






        private string HashPassword(string password)
        {

            using SHA256 sha = SHA256.Create();


            byte[] bytes =
            sha.ComputeHash(
            Encoding.UTF8.GetBytes(password));


            return Convert.ToBase64String(bytes);

        }





        private bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }

    }
}