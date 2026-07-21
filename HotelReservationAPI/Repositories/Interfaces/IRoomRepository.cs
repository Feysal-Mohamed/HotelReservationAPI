using HotelReservationAPI.MODEL;

namespace HotelReservationAPI.Repositories.Interfaces
{
    public interface IRoomRepository
    {
        string AddRoom(Room room);

        List<Room> GetAllRooms();

        Room GetRoomById(int id);

        string UpdateRoom(Room room);

        string DeleteRoom(int id);
    }
}