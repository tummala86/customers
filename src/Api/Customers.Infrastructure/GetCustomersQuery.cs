using Customers.Domain.Models;
using Customers.Domain.Ports;
using Customers.Infrastructure.Extensions;
using Customers.Infrastructure.Ports;

namespace Customers.Infrastructure
{
    public class GetCustomersQuery : IGetCustomersQuery
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomersQuery(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<GetCustomerResponse> GetCustomerById(GetCustomerRequest request)
        {
            var getCustomerResponse = await _customerRepository.GetByIdAsync(request.Id);

            return getCustomerResponse.Match<GetCustomerResponse>(
                success => new GetCustomerResponse.Success(success.Customer!.ToDomainCustomer()),
                notFound => new GetCustomerResponse.NotFound(),
                internalError => new GetCustomerResponse.InternalError(internalError.ErrorMessage));
        }

        public async Task<GetCustomersResponse> GetAllCustomers()
        {
            var getCustomersResponse = await _customerRepository.GetAllAsync();

            return getCustomersResponse.Match<GetCustomersResponse>(
                success => new GetCustomersResponse.Success(success.Customers!.ToDomainCustomers()),
                internalError => new GetCustomersResponse.InternalError(internalError.ErrorMessage));
        }
    }
}
