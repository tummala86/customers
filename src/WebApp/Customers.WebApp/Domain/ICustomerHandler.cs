using Customers.WebApp.Domain.Models;

namespace Customers.WebApp.Domain
{
    public interface ICustomerHandler
    {
        Task<GetCustomersResponse> GetAllCustomers();
        Task<GetCustomerResponse> GetCustomerById(string id);
        Task<CreateCustomerResponse> AddCustomer(Customer customer);
        Task<UpdateCustomerResponse> UpdateCustomer(Customer customer);
        Task<UpdateCustomerResponse> DeleteCustomer(string id);
    }
}
