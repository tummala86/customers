using Customers.Test.Integration.Fixtures;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Customers.Test.Integration.GetCustomer
{
    public class GetCustomerFailureTest : TestServerFixture
    {
        [Fact]
        public async Task GetCustomer_Should_Return_NotFound_If_Invalid_Customer_Id()
        {
            // Arrange
            var client = Server.CreateClient();

            // Act
            var results = await client.GetAsync($"v3/customers/{Guid.NewGuid().ToString()}");

            // Assert
            results.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
