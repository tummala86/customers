using Customers.Infrastructure.Database.Entities;
using Customers.Infrastructure.Extensions;
using FluentAssertions;
using Xunit;

namespace Customers.API.Test.Unit.Infrastructure.Extensions
{
    public class GetCustomerExtensionsTests
    {
        [Fact]
        public void ToDomainCustomer_ShouldMapCustomerDetails()
        {
            // Arrange
            var customerDetails = new Customer()
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "John@gmail.com"
            };

            // Act
            var result = customerDetails.ToDomainCustomer();

            // Assert
            result.Should().NotBeNull();
            result.CustomerId.Should().NotBeEmpty();
            result.FirstName.Should().Be(customerDetails.FirstName);
            result.LastName.Should().Be(customerDetails.LastName);
            result.Email.Should().Be(customerDetails.Email);
        }

        [Fact]
        public void ToDomainCustomer_ShouldMapCustomersList()
        {
            // Arrange
            var customerDetails = new Customer()
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "John@gmail.com"
            };

            var customers = new List<Customer>() { customerDetails };

            // Act
            var result = customers.ToDomainCustomers();

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().Be(customers.Count());
            result.FirstOrDefault()?.CustomerId.Should().NotBeEmpty();
            result.FirstOrDefault()?.FirstName.Should().Be(customerDetails.FirstName);
            result.FirstOrDefault()?.LastName.Should().Be(customerDetails.LastName);
            result.FirstOrDefault()?.Email.Should().Be(customerDetails.Email);
        }
    }
}
