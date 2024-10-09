using Customers.Domain.Models;

namespace Customers.Domain.Ports;

public interface IGetCustomersQuery
{
    Task<GetCustomersResponse> GetAllCustomers();
    Task<GetCustomerResponse> GetCustomerById(GetCustomerRequest request);
}
