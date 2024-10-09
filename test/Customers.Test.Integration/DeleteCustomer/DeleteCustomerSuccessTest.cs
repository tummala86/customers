using Customers.Domain.Models;
using Customers.Test.Integration.Fixtures;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Xunit;

namespace Customers.Test.Integration.DeleteCustomer
{
    public class DeleteCustomerSuccessTest : TestServerFixture
    {
        [Fact]
        public async Task DeleteCustomer_Should_Return_Success()
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

            var response = await client.PostAsJsonAsync("v3/customers", customerRequest);
            var body = await response.Content.ReadAsStringAsync();
            var customerDetails = JsonSerializer.Deserialize<Customer>(body);

            // Act
            var results = await client.DeleteAsync($"v3/customers/{customerDetails?.CustomerId}");

            // Assert
            results.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
