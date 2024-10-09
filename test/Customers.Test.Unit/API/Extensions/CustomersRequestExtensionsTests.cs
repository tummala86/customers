using Customers.API.Models;
using Customers.API.Extensions;
using Xunit;
using FluentAssertions;

namespace Customers.API.Test.Unit.API.Extensions
{
    public class CustomersRequestExtensionsTests
    {
        [Theory]
        [MemberData(nameof(CustomerRequest))]
        public void ToDomain_ShouldMapCustomerRequestToDomainCustomerRequest(CustomerRequest customerRequest)
        {
            // Arrange && Act
            var result = customerRequest.ToDomain();

            // Assert
            result.Should().NotBeNull();
            result.CustomerId.Should().NotBeEmpty();
            result.FirstName.Should().Be(customerRequest.FirstName);
            result.LastName.Should().Be(customerRequest.LastName);
            result.Email.Should().Be(customerRequest.Email);
        }

        public static IEnumerable<object[]> CustomerRequest => new List<object[]>
        {
            new object[] { new CustomerRequest(Guid.NewGuid().ToString(), "John", "Doe", "johndoe@gmail.com") },
            new object[] { new CustomerRequest(null, "john", "Doe", "johndoe@gmail.com") },
        };
    }
}
