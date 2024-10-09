using Customers.Infrastructure.Database;
using Customers.Infrastructure.Extensions;
using Customers.Infrastructure.Models;
using Customers.Infrastructure.Ports;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DomainCustomer = Customers.Domain.Models.Customer;

namespace Customers.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ILogger<CustomerRepository> _logger;

        public CustomerRepository(ILogger<CustomerRepository> logger)
        {
            _logger = logger;
        }

        public async Task<GetCustomersResponse> GetAllAsync()
        {
            try
            {
                using (var context = new CustomersDbContext())
                {
                    var result = await context.Customers.Where(p => p.IsActive).ToListAsync();
                    return new GetCustomersResponse.Success(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("There is an exception while fetching customers.", ex);
                return new GetCustomersResponse.InternalError(ex.Message);
            }
        }

        public async Task<GetCustomerResponse> GetByIdAsync(Guid customerId)
        {
            try
            {
                using (var context = new CustomersDbContext())
                {
                    var result = await context.Customers.Where(c=> c.Id == customerId && c.IsActive).FirstOrDefaultAsync();

                    if(result != null)
                        return new GetCustomerResponse.Success(result);

                    return new GetCustomerResponse.NotFound();
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"There is an error while fetching the customer with Id: {customerId}", ex);
                return new GetCustomerResponse.InternalError(ex.Message);
            }
        }

        public async Task<CreateCustomerResponse> InsertAsync(DomainCustomer customer)
        {
            try
            {
                using (var context = new CustomersDbContext())
                {
                    var customerDbEntity = customer.ToCustomerDbEntity();
                    await context.Customers.AddAsync(customerDbEntity);
                    await context.SaveChangesAsync();
                    return new CreateCustomerResponse.Success(customerDbEntity);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"There is an error while saving the customer details: {customer.CustomerId}", ex);
                return new CreateCustomerResponse.InternalError(ex.Message);
            }
            
        }

        public async Task<UpdateCustomerResponse> UpdateAsync(DomainCustomer customer)
        {
            try
            {
                var getCustomerResponse = await GetByIdAsync(customer.CustomerId);

                return await getCustomerResponse.Match<Task<UpdateCustomerResponse>>(
                   async success =>
                   {
                       var customerDetails = success.Customer!;
                        customerDetails.FirstName = customer.FirstName;
                        customerDetails.LastName = customer.LastName;
                        customerDetails.Email = customer.Email;
                        customerDetails.UpdatedAt = DateTime.UtcNow;

                        using (var context = new CustomersDbContext())
                        {
                            context.Customers.Update(customerDetails);
                            await context.SaveChangesAsync();
                        }
                      
                       return new UpdateCustomerResponse.Success(customerDetails);
                   },
                    notFound => Task.FromResult<UpdateCustomerResponse>(new UpdateCustomerResponse.NotFound()),
                    internalError => Task.FromResult<UpdateCustomerResponse>(new UpdateCustomerResponse.InternalError(internalError.ErrorMessage)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"There is an error while updating the customer details: {customer.CustomerId}", ex);
                return new UpdateCustomerResponse.InternalError(ex.Message);
            }
        }

        public async Task<UpdateCustomerResponse> DeleteAsync(Guid customerId)
        {
            try
            {
                var getCustomerResponse = await GetByIdAsync(customerId);

                return await getCustomerResponse.Match<Task<UpdateCustomerResponse>>(
                   async success =>
                    {
                        var customerDetails = success.Customer!;
                        customerDetails.IsActive = false;
                        customerDetails.UpdatedAt = DateTime.UtcNow;

                        using (var context = new CustomersDbContext())
                        {
                            context.Customers.Update(customerDetails);
                            await context.SaveChangesAsync();
                        }

                        return new UpdateCustomerResponse.Success(customerDetails);
                    },
                    notFound => Task.FromResult<UpdateCustomerResponse>(new UpdateCustomerResponse.NotFound()),
                    internalError => Task.FromResult<UpdateCustomerResponse>(new UpdateCustomerResponse.InternalError(internalError.ErrorMessage)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"There is an error while deleting the customer : {customerId}", ex);
                return new UpdateCustomerResponse.InternalError(ex.Message);
            }
        }
    }
}
