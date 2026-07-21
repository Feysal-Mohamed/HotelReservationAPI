using HotelReservationAPI.MODEL;

namespace HotelReservationAPI.Services.Interfaces
{
    public interface IRoomService
    {
        string Add(Room room);
        List<Room> GetAll();
        Room GetById(int id);
        string Update(Room room);
        string Delete(int id);
    }
}