using HotelReservationAPI.MODEL;

using HotelReservationAPI.Repositories.Interfaces;
using HotelReservationAPI.Services.Interfaces;


namespace HotelReservationAPI.Services
{
    public class ReservationService : IReservationService
    {

        private readonly IReservationRepository repository;



        public ReservationService(IReservationRepository repository)
        {
            this.repository = repository;
        }



        public string Add(Reservation reservation)
        {
            return repository.AddReservation(reservation);
        }



        public List<Reservation> GetAll()
        {
            return repository.GetAllReservations();
        }



        public Reservation GetById(int id)
        {
            return repository.GetReservationById(id);
        }



        public string Update(Reservation reservation)
        {
            return repository.UpdateReservation(reservation);
        }



        public string Delete(int id)
        {
            return repository.DeleteReservation(id);
        }

    }
}