using Customers.Domain.Models;
using Customers.Test.Integration.Fixtures;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Xunit;

namespace Customers.Test.Integration.UpdateCustomer
{
    public class UpdateCustomerSuccessTest : TestServerFixture
    {
        [Fact]
        public async Task UpdateCustomer_Should_Return_Success()
        {
            // Arrange
            var client = Server.CreateClient();
            var customerRequest=
                new
                {
                    first_name = "John",
                    last_name = "Doe",
                    email = "test@gmail.com",
                };

            var response = await client.PostAsJsonAsync("v3/customers/update", customerRequest);
            var body = await response.Content.ReadAsStringAsync();
            var customerDetails = JsonSerializer.Deserialize<Customer>(body);

            var updateCustomerRequest = CustomerRequest(customerDetails!.CustomerId, firstName: "Robert");

            // Act
            var results = await client.PutAsJsonAsync($"v3/customers", updateCustomerRequest);

            // Assert
            results.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        private object CustomerRequest(Guid customerId ,string? firstName = "John", string? lastName = "Doe",
           string? email = "test@gmail.com")
        {
            return new
            {
                id = customerId,
                first_name = firstName,
                last_name = lastName,
                email = email,
            };
        }
    }
}
