using HotelReservationAPI.MODEL;

namespace HotelReservationAPI.Services.Interfaces
{
    public interface ICustomerService
    {
        string Add(Customer customer);

        List<Customer> GetAll();

        Customer GetById(int id);

        string Update(Customer customer);

        string Delete(int id);
    }
}