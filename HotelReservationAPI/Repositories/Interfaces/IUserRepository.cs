using HotelReservationAPI.MODEL;

namespace HotelReservationAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        bool CheckUserExists(string username, string email);

        string AddUser(User user);

        List<User> GetAllUsers();

        User GetUserByEmail(string email);

        User GetUserById(int id);

        string UpdateUser(User user);

        string DeleteUser(int id);
    }
}