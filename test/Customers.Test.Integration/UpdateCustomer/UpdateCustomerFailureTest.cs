using Customers.Test.Integration.Fixtures;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Customers.Test.Integration.UpdateCustomer
{
    public class UpdateCustomerFailureTest : TestServerFixture
    {
        [Fact]
        public async Task UpdateCustomer_Should_Return_NotFound_If_Invalid_Customer_Id()
        {
            // Arrange
            var client = Server.CreateClient();

            var updateCustomerRequest = CustomerRequest(Guid.NewGuid(), firstName: "Robert");

            // Act
            var results = await client.PutAsJsonAsync($"v3/customers/update", updateCustomerRequest);

            // Assert
            results.StatusCode.Should().Be(HttpStatusCode.NotFound);
            var body = await results.Content.ReadAsStringAsync();
            body.Should().NotBeNull();
        }

        private object CustomerRequest(Guid customerId, string? firstName = "John", string? lastName = "Doe",
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
