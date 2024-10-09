using Customers.WebApp.Domain.Models;

namespace Customers.WebApp.Infrastructure
{
    public interface ICustomerClient
    {
        Task<GetCustomersResponse> GetAllCustomers();
        Task<GetCustomerResponse> GetCustomerById(string id);
        Task<CreateCustomerResponse> AddCustomer(Customer customer);
        Task<UpdateCustomerResponse> UpdateCustomer(Customer customer);
        Task<UpdateCustomerResponse> DeleteCustomer(string id);
    }
}
