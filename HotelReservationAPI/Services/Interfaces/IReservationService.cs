using HotelReservationAPI.MODEL;
namespace HotelReservationAPI.Services.Interfaces
{
    public interface IReservationService
    {

        string Add(Reservation reservation);

        List<Reservation> GetAll();

        Reservation GetById(int id);

        string Update(Reservation reservation);

        string Delete(int id);

    }
}