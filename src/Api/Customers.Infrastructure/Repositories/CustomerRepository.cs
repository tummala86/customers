using Customers.Infrastructure.Database;
using Customers.Infrastructure.Database.Entities;
using Customers.Infrastructure.Ports;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Customers.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ILogger<CustomerRepository> _logger;

        public CustomerRepository(ILogger<CustomerRepository> logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<Customer>?> GetAllAsync()
        {
            using (var context = new CustomersDbContext())
            {
                var result = await context.Customers.Where(p => p.IsActive).ToListAsync();
                return result;
            }
        }

        public async Task<Customer?> GetByIdAsync(Guid customerId)
        {
            using (var context = new CustomersDbContext())
            {
                return await context.Customers.Where(c => c.Id == customerId && c.IsActive).FirstOrDefaultAsync();
            }
                    
        }

        public async Task<Customer> InsertAsync(Customer customer)
        {
            using (var context = new CustomersDbContext())
            {
                await context.Customers.AddAsync(customer);
                await context.SaveChangesAsync();
                return customer;
            }        
        }

        public async Task<Customer?> UpdateAsync(Customer customer)
        {
            var customerDetails = await GetByIdAsync(customer.Id);

            if (customerDetails != null)
            {
                customerDetails.FirstName = customer.FirstName;
                customerDetails.LastName = customer.LastName;
                customerDetails.Email = customer.Email;
                customerDetails.UpdatedAt = DateTime.UtcNow;

                using (var context = new CustomersDbContext())
                {
                    context.Customers.Update(customerDetails);
                    await context.SaveChangesAsync();
                }
            }

            return customerDetails;
        }

        public async Task<Customer?> DeleteAsync(Guid customerId)
        {
            var customer = await GetByIdAsync(customerId);
            if (customer != null)
            {
                customer!.IsActive = false;
                customer!.UpdatedAt = DateTime.UtcNow;

                using (var context = new CustomersDbContext())
                {
                    context.Customers.Update(customer);
                    await context.SaveChangesAsync();
                }
            }

            return customer;
        }
    }
}
