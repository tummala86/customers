using Customers.Domain.Models;

namespace Customers.Domain.Handlers
{
    public interface ICustomerCommandHandler
    {
        Task<CreateCustomerResponse> SaveCustomer(Customer customer);
        Task<UpdateCustomerResponse> UpdateCustomer(Customer customer);
        Task<UpdateCustomerResponse> DeleteCustomer(Guid customerId);
    }
}