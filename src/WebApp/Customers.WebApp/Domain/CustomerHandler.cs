using Customers.WebApp.Domain.Models;
using Customers.WebApp.Infrastructure;

namespace Customers.WebApp.Domain;

public class CustomerHandler: ICustomerHandler
{
    private readonly ICustomerClient _customerClient;


    public CustomerHandler(ICustomerClient customerClient)
    {
        _customerClient = customerClient;
    }

    public async Task<GetCustomersResponse> GetAllCustomers()
    {
        return await _customerClient.GetAllCustomers();
    }

    public async Task<GetCustomerResponse> GetCustomerById(string id)
    {
        return await _customerClient.GetCustomerById(id);
    }

    public async Task<CreateCustomerResponse> AddCustomer(Customer customer)
    {
        return await _customerClient.AddCustomer(customer);
    }

    public async Task<UpdateCustomerResponse> UpdateCustomer(Customer customer)
    {
        return await _customerClient.UpdateCustomer(customer);
    }

    public async Task<UpdateCustomerResponse>DeleteCustomer(string id)
    {
        return await _customerClient.DeleteCustomer(id);
    }
}
