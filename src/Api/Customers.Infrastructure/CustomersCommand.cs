using Customers.Domain.Ports;
using Customers.Infrastructure.Ports;
using Customers.Domain.Models;
using Customers.Infrastructure.Extensions;

namespace Customers.Infrastructure
{
    public class CustomersCommand : ICustomerCommand
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersCommand(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<UpdateCustomerResponse> DeleteCustomer(Guid customerId)
        {
            var updateCustomerResponse = await _customerRepository.DeleteAsync(customerId);

            return updateCustomerResponse.Match<UpdateCustomerResponse>(
                success => new UpdateCustomerResponse.Success(success.Customer!.ToDomainCustomer()),
                notFound => new UpdateCustomerResponse.NotFound(),
                internalError => new UpdateCustomerResponse.InternalError(internalError.ErrorMessage));
        }

        public async Task<CreateCustomerResponse> SaveCustomer(Customer customer)
        {
            var createCustomerResponse = await _customerRepository.InsertAsync(customer);

            return createCustomerResponse.Match<CreateCustomerResponse>(
                success => new CreateCustomerResponse.Success(success.Customer!.ToDomainCustomer()),
                internalError => new CreateCustomerResponse.InternalError(internalError.ErrorMessage));
        }

        public async Task<UpdateCustomerResponse> UpdateCustomer(Customer customer)
        {
            var updateCustomerResponse = await _customerRepository.UpdateAsync(customer);

            return updateCustomerResponse.Match<UpdateCustomerResponse>(
                success => new UpdateCustomerResponse.Success(success.Customer!.ToDomainCustomer()),
                notFound => new UpdateCustomerResponse.NotFound(),
                internalError => new UpdateCustomerResponse.InternalError(internalError.ErrorMessage));
        }
    }
}
