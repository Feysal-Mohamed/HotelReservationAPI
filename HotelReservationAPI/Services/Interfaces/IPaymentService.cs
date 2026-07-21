using HotelReservationAPI.MODEL;


namespace HotelReservationAPI.Services.Interfaces
{
    public interface IPaymentService
    {

        string Add(Payment payment);

        List<Payment> GetAll();

        Payment GetById(int id);

        string Update(Payment payment);

        string Delete(int id);

    }
}