using Customers.Domain.Models;
using Customers.Domain.Ports;

namespace Customers.Domain.Handlers
{
    public class CustomerCommandHandler : ICustomerCommandHandler
    {
        private readonly ICustomerCommand _customerCommand;

        public CustomerCommandHandler(ICustomerCommand customerCommand)
        {
            _customerCommand = customerCommand;
        }

        public async Task<CreateCustomerResponse> SaveCustomer(Customer customer)
        {
            return await _customerCommand.SaveCustomer(customer);
        }

        public async Task<UpdateCustomerResponse> UpdateCustomer(Customer customer)
        {
            return await _customerCommand.UpdateCustomer(customer);
        }

        public async Task<UpdateCustomerResponse> DeleteCustomer(Guid customerId)
        {
            return await _customerCommand.DeleteCustomer(customerId);
        }

    }
}
