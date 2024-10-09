using Customers.WebApp.Domain.Models;
using Customers.WebApp.Infrastructure.Extensions;
using Customers.WebApp.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Customers.WebApp.Infrastructure;

public class CustomerClient: ICustomerClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public CustomerClient(HttpClient httpClient)
    {
        _httpClient = httpClient;

    }

    public async Task<GetCustomerResponse> GetCustomerById(string id)
    {
        try
        {
            var httpResponse = await _httpClient.GetAsync($"v3/customers/{id}");

            var content = await httpResponse.Content.ReadAsStringAsync();

            if (httpResponse.IsSuccessStatusCode)
            {
                var customerDetails = JsonSerializer.Deserialize<CustomerResponse>(content, jsonSerializerOptions);
                return new GetCustomerResponse.Success(customerDetails!.ToDomain());
            }

            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(content, jsonSerializerOptions);
            return new GetCustomerResponse.Error(problemDetails?.Detail!);

        }
        catch(Exception ex)
        {
            return new GetCustomerResponse.Error(ex.Message);
        }
    }

    public async Task<GetCustomersResponse> GetAllCustomers()
    {
        try
        {
            var httpResponse = await _httpClient.GetAsync("v3/customers");

            var content = await httpResponse.Content.ReadAsStringAsync();

            if (httpResponse.IsSuccessStatusCode)
            {
                var customerDetails = JsonSerializer.Deserialize<IEnumerable<CustomerResponse>>(content, jsonSerializerOptions);
                return new GetCustomersResponse.Success(customerDetails!.ToDomain());
            }

            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(content, jsonSerializerOptions);
            return new GetCustomersResponse.Error(problemDetails?.Detail!);
        }
        catch(Exception ex)
        {
            return new GetCustomersResponse.Error(ex.Message);
        }
    }

    public async Task<CreateCustomerResponse> AddCustomer(Customer customer)
    {
        try
        {
            var customerRequest = new CustomerRequest{
               FirstName= customer.FirstName, 
               LastName= customer.LastName, 
               Email = customer.Email 
            };

            string data = JsonSerializer.Serialize(customerRequest, jsonSerializerOptions);
            var jsonContent = new StringContent(data, Encoding.UTF8, "application/json");

            var httpResponse = await _httpClient.PostAsync("v3/customers/add", jsonContent);

            if (httpResponse.IsSuccessStatusCode)
            {
                return new CreateCustomerResponse.Success();
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(content, jsonSerializerOptions);
            return new CreateCustomerResponse.Error(problemDetails?.Detail!);
        }
        catch (Exception ex)
        {
            return new CreateCustomerResponse.Error(ex.Message);
        }
    }

    public async Task<UpdateCustomerResponse> UpdateCustomer(Customer customer)
    {
        try
        {
            var customerRequest = new CustomerRequest
            {
                Id = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email
            };

            string data = JsonSerializer.Serialize(customerRequest, jsonSerializerOptions);
            var stringContent = new StringContent(data, Encoding.UTF8, "application/json");

            var httpResponse = await _httpClient.PutAsync($"v3/customers/update", stringContent);

            if (httpResponse.IsSuccessStatusCode)
            {
                return new UpdateCustomerResponse.Success();
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(content, jsonSerializerOptions);
            return new UpdateCustomerResponse.Error(problemDetails?.Detail!);
        }
        catch (Exception ex)
        {
            return new UpdateCustomerResponse.Error(ex.Message);
        }
    }

    public async Task<UpdateCustomerResponse> DeleteCustomer(string id)
    {
        try
        {
            var httpResponse = await _httpClient.DeleteAsync($"v3/customers/{id}");

            if (httpResponse.IsSuccessStatusCode)
            {
                return new UpdateCustomerResponse.Success();
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(content, jsonSerializerOptions);
            return new UpdateCustomerResponse.Error(problemDetails?.Detail!);
        }
        catch (Exception ex)
        {
            return new UpdateCustomerResponse.Error(ex.Message);
        }
    }
}
