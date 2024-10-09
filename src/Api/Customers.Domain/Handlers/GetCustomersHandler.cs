using Customers.Domain.Models;
using Customers.Domain.Ports;

namespace Customers.Domain.Handlers
{
    public class GetCustomersHandler : IGetCustomersQueryHandler
    {
        private readonly IGetCustomersQuery _getCustomersQuery;

        public GetCustomersHandler(IGetCustomersQuery getCustomersQuery)
        {
            _getCustomersQuery = getCustomersQuery;
        }

        public async Task<GetCustomersResponse> GetAllCustomers()
        {
            return await _getCustomersQuery.GetAllCustomers();
        }

        public async Task<GetCustomerResponse> GetCustomerById(GetCustomerRequest request)
        {
            return await _getCustomersQuery.GetCustomerById(request);
        }
    }
}
