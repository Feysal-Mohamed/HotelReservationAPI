using HotelReservationAPI.MODEL;

namespace HotelReservationAPI.Services.Interfaces
{
    public interface IUserService
    {
        string Register(User user);

        object Login(LoginRequest request);

        List<User> GetAll();

        User GetById(int id);

        string Update(int id, User user);

        string Delete(int id);
    }
}