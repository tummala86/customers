using Customers.Domain.Models;
using Customers.Test.Integration.Fixtures;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Xunit;

namespace Customers.Test.Integration.GetCustomers
{
    public class GetCustomersSuccessTest : TestServerFixture
    {
        [Fact]
        public async Task GetCustomers_Should_Return_Success()
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

            // Act
            var results = await client.GetAsync($"v3/customers");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            results.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
