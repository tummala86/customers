using Customers.Domain.Models;

namespace Customers.Domain.Handlers
{
    public interface IGetCustomersQueryHandler
    {
        Task<GetCustomersResponse> GetAllCustomers();
        Task<GetCustomerResponse> GetCustomerById(GetCustomerRequest request);
    }
}