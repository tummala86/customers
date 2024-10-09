using Customers.API.Validation;
using FluentAssertions;
using Xunit;

namespace Customers.API.Test.Unit.API.Validation
{
    public class GetCustomerRequestValidatorTests
    {
        [Fact]
        public void ValidateAsync_RetrunsSuccess()
        {
            // Arrange
            var customerId = Guid.NewGuid().ToString();

            // Act
            var result = GetCustomerRequestValidator.Validate(customerId);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("test_id")]
        public void ValidateAsync_RetrunsFailure(string? customerId)
        {
            // Arrange && Act
            var result = GetCustomerRequestValidator.Validate(customerId);

            // Assert
            result.IsFailure.Should().BeTrue();
        }
    }
}
