using HotelReservationAPI.MODEL;
using HotelReservationAPI.Repositories.Interfaces;
using HotelReservationAPI.Services.Interfaces;

namespace HotelReservationAPI.Services
{
    public class RoomService : IRoomService
    {

        private readonly IRoomRepository repository;


        public RoomService(IRoomRepository repository)
        {
            this.repository = repository;
        }


        public string Add(Room room)
        {
            return repository.AddRoom(room);
        }


        public List<Room> GetAll()
        {
            return repository.GetAllRooms();
        }


        public Room GetById(int id)
        {
            return repository.GetRoomById(id);
        }


        public string Update(Room room)
        {
            return repository.UpdateRoom(room);
        }


        public string Delete(int id)
        {
            return repository.DeleteRoom(id);
        }

    }
}