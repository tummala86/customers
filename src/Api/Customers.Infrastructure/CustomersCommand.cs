using Customers.Domain.Ports;
using Customers.Infrastructure.Ports;
using Customers.Infrastructure.Extensions;
using DomainCustomer = Customers.Domain.Models.Customer;
using Customers.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Customers.Infrastructure
{
    public class CustomersCommand : ICustomerCommand
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CustomersCommand> _logger;

        public CustomersCommand(
            ICustomerRepository customerRepository,
            ILogger<CustomersCommand> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public async Task<UpdateCustomerResponse> DeleteCustomer(Guid customerId)
        {
            try
            {
                var deleteCustomerResponse = await _customerRepository.DeleteAsync(customerId);

                if (deleteCustomerResponse is not null)
                {
                    return new UpdateCustomerResponse.Success(deleteCustomerResponse.ToDomainCustomer());
                }
                else
                {
                    return new UpdateCustomerResponse.NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"There is an error while deleting the customer : {customerId}");
                return new UpdateCustomerResponse.InternalError(ex.Message);
            }
        }

        public async Task<CreateCustomerResponse> SaveCustomer(DomainCustomer domainCustomer)
        {
            try
            {
                var customerDbEntity = domainCustomer.ToCustomerDbEntity();
                var createCustomerResponse = await _customerRepository.InsertAsync(customerDbEntity);

                if (createCustomerResponse is not null)
                {
                    return new CreateCustomerResponse.Success(createCustomerResponse.ToDomainCustomer());
                }

                return new CreateCustomerResponse.InternalError("There is an error while saving the customer");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There is an error while saving the customer");
                return new CreateCustomerResponse.InternalError(ex.Message);
            }
        }

        public async Task<UpdateCustomerResponse> UpdateCustomer(DomainCustomer domainCustomer)
        {
            try
            {
                var customerDbEntity = domainCustomer.ToCustomerDbEntity();
                var updateCustomerResponse = await _customerRepository.UpdateAsync(customerDbEntity);

                if (updateCustomerResponse is not null)
                {
                    return new UpdateCustomerResponse.Success(updateCustomerResponse.ToDomainCustomer());
                }
                else
                {
                    return new UpdateCustomerResponse.NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"There is an error while deleting the customer : {domainCustomer.CustomerId}");
                return new UpdateCustomerResponse.InternalError(ex.Message);
            }
        }
    }
}
