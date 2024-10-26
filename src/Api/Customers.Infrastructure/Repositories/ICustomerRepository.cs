using Customers.Infrastructure.Database.Entities;

namespace Customers.Infrastructure.Ports
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>?> GetAllAsync();
        Task<Customer?> GetByIdAsync(Guid customerId);
        Task<Customer> InsertAsync(Customer customer);
        Task<Customer?> UpdateAsync(Customer customer);
        Task<Customer?> DeleteAsync(Guid customerId);
    }
}
