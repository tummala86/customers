using Customers.Domain.Models;
using Customers.Domain.Ports;
using Customers.Infrastructure.Extensions;
using Customers.Infrastructure.Ports;
using Microsoft.Extensions.Logging;

namespace Customers.Infrastructure
{
    public class GetCustomersQuery : IGetCustomersQuery
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<GetCustomersQuery> _logger;

        public GetCustomersQuery(
            ICustomerRepository customerRepository,
            ILogger<GetCustomersQuery> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public async Task<GetCustomerResponse> GetCustomerById(GetCustomerRequest request)
        {
            try
            {
                var getCustomerResponse = await _customerRepository.GetByIdAsync(request.Id);

                if (getCustomerResponse is not null) 
                {
                    return new GetCustomerResponse.Success(getCustomerResponse.ToDomainCustomer());
                }

                return new GetCustomerResponse.NotFound();
                   
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"There is an error while fetching the customer : {request.Id}");
                return new GetCustomerResponse.InternalError(ex.Message);
            }
        }

        public async Task<GetCustomersResponse> GetAllCustomers()
        {
            try
            {
                var getCustomersResponse = await _customerRepository.GetAllAsync();

                if(getCustomersResponse is not null)
                {
                    return new GetCustomersResponse.Success(getCustomersResponse.ToDomainCustomers());
                }

               return new GetCustomersResponse.InternalError("There is an error while fetching get all customers");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"There is an error while fetching get all customers");
                return new GetCustomersResponse.InternalError(ex.Message);
            }
        }
    }
}
