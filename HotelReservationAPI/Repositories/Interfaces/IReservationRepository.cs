using HotelReservationAPI.MODEL;

namespace HotelReservationAPI.Repositories.Interfaces
{
    public interface IReservationRepository
    {
        string AddReservation(Reservation reservation);

        List<Reservation> GetAllReservations();

        Reservation GetReservationById(int id);

        string UpdateReservation(Reservation reservation);

        string DeleteReservation(int id);
    }
}