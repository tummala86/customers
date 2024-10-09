using Customers.Domain.Models;
using Customers.Infrastructure.Extensions;
using FluentAssertions;
using Xunit;

namespace Customers.API.Test.Unit.Infrastructure.Extensions
{
    public class CustomerExtensionsTests
    {
        [Fact]
        public void ToCustomerDbEntity_ShouldMapDomainCustomerRequestToInfraCustomerRequest()
        {
            // Arrange
            var customerRequest = new Customer(Guid.NewGuid(), "John", "Doe", "johndoe@gmail.com");

            // Act
            var result = customerRequest.ToCustomerDbEntity();

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().NotBeEmpty();
            result.IsActive.Should().BeTrue();
            result.FirstName.Should().Be(customerRequest.FirstName);
            result.LastName.Should().Be(customerRequest.LastName);
            result.Email.Should().Be(customerRequest.Email);
        }
    }
}
