using Customers.Test.Integration.Fixtures;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Customers.Test.Integration.CreateCustomer
{
    public class CreateCustomerSuccessTest : TestServerFixture
    {
        [Fact]
        public async Task CreateCustomer_Should_Return_Success()
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

            // Act
            var response = await client.PostAsJsonAsync("v3/customers/add", customerRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created); 
            var body = await response.Content.ReadAsStringAsync();
            body.Should().NotBeNull();
        }
    }
}
