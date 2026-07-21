using HotelReservationAPI.MODEL;
using HotelReservationAPI.Repositories.Interfaces;
using HotelReservationAPI.Services.Interfaces;


namespace HotelReservationAPI.Services
{
    public class PaymentService : IPaymentService
    {

        private readonly IPaymentRepository repository;



        public PaymentService(IPaymentRepository repository)
        {
            this.repository = repository;
        }



        public string Add(Payment payment)
        {
            return repository.AddPayment(payment);
        }



        public List<Payment> GetAll()
        {
            return repository.GetAllPayments();
        }



        public Payment GetById(int id)
        {
            return repository.GetPaymentById(id);
        }



        public string Update(Payment payment)
        {
            return repository.UpdatePayment(payment);
        }



        public string Delete(int id)
        {
            return repository.DeletePayment(id);
        }

    }
}