using DomainCustomer = Customers.Domain.Models.Customer;
using Customers.Infrastructure.Models;

namespace Customers.Infrastructure.Ports
{
    public interface ICustomerRepository
    {
        Task<GetCustomersResponse> GetAllAsync();
        Task<GetCustomerResponse> GetByIdAsync(Guid customerId);
        Task<CreateCustomerResponse> InsertAsync(DomainCustomer customer);
        Task<UpdateCustomerResponse> UpdateAsync(DomainCustomer customer);
        Task<UpdateCustomerResponse> DeleteAsync(Guid customerId);
    }
}
