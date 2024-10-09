using Customers.API.Constants;
using Customers.Test.Integration.Fixtures;
using FluentAssertions;
using System.Net;
using Xunit;

namespace Customers.Test.Integration
{
    public class HealthCheckTest : TestServerFixture
    {
        [Fact]
        public async Task TestHealthCheck()
        {
            // Arrange
            var httpClient = Server.CreateClient();

            // Act
            var response = await httpClient.GetAsync(ApiRoutes.HealthChecks.Internal);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
