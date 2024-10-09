using Customers.Domain.Models;

namespace Customers.Domain.Ports
{
    public interface ICustomerCommand
    {
        Task<CreateCustomerResponse> SaveCustomer(Customer customer);
        Task<UpdateCustomerResponse> UpdateCustomer(Customer customer);
        Task<UpdateCustomerResponse> DeleteCustomer(Guid customerId);
    }
}
