using HotelReservationAPI.MODEL;


namespace HotelReservationAPI.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        string AddPayment(Payment payment);

        List<Payment> GetAllPayments();

        Payment GetPaymentById(int id);

        string UpdatePayment(Payment payment);

        string DeletePayment(int id);
    }
}