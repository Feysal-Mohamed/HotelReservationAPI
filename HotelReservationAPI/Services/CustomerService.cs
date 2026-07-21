using HotelReservationAPI.MODEL;
using HotelReservationAPI.Repositories.Interfaces;
using HotelReservationAPI.Services.Interfaces;


namespace HotelReservationAPI.Services
{
    public class CustomerService : ICustomerService
    {

        private readonly ICustomerRepository repository;



        public CustomerService(ICustomerRepository repository)
        {
            this.repository = repository;
        }



        public string Add(Customer customer)
        {
            return repository.AddCustomer(customer);
        }



        public List<Customer> GetAll()
        {
            return repository.GetAllCustomers();
        }



        public Customer GetById(int id)
        {
            return repository.GetCustomerById(id);
        }



        public string Update(Customer customer)
        {
            return repository.UpdateCustomer(customer);
        }



        public string Delete(int id)
        {
            return repository.DeleteCustomer(id);
        }

    }
}