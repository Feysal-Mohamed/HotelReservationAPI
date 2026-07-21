using HotelReservationAPI.MODEL;

namespace HotelReservationAPI.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        string AddCustomer(Customer customer);

        List<Customer> GetAllCustomers();

        Customer GetCustomerById(int id);

        string UpdateCustomer(Customer customer);

        string DeleteCustomer(int id);
    }
}