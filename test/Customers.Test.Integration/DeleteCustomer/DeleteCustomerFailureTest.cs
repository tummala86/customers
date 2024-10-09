using Customers.Test.Integration.Fixtures;
using FluentAssertions;
using System.Net;
using Xunit;

namespace Customers.Test.Integration.DeleteCustomer
{
    public class DeleteCustomerFailureTest : TestServerFixture
    {
        [Fact]
        public async Task DeleteCustomer_Should_Return_NotFound_If_Invalid_Customer_Id()
        {
            // Arrange
            var client = Server.CreateClient();

            // Act
            var response = await client.DeleteAsync($"v3/customers/{Guid.NewGuid().ToString()}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            var body = await response.Content.ReadAsStringAsync();
            body.Should().NotBeNull();
        }

    }
}
