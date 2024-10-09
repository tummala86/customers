using Customers.Test.Integration.Fixtures;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Customers.Test.Integration.CreateCustomer
{
    public class CreateCustomerFailureTest : TestServerFixture
    {
        [Fact]
        public async Task CreateCustomer_Should_Return_Invalid_Parameters_If_Invalid_Request()
        {
            // Arrange
            var client = Server.CreateClient();

            var customerRequest = CustomerRequest(firstName: null);

            // Act
            var response = await client.PostAsJsonAsync("v3/customers/add", customerRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var body = await response.Content.ReadAsStringAsync();
            body.Should().NotBeNull();
        }

        private object CustomerRequest(string? firstName = "John", string? lastName= "Doe",
            string? email="test@gmail.com")
        {
            return new
            {
                first_name = firstName,
                last_name = lastName,
                email = email,
            };
        }
    }
}
