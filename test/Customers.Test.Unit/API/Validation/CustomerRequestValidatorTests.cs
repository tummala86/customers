using Customers.API.Models;
using Customers.API.Validation;
using FluentAssertions;
using Xunit;

namespace Customers.Test.Unit.API.Validation
{
    public class CustomerRequestValidatorTests
    {
        [Fact]
        public void ValidateAsync_ValidAddCustomerRequest_RetrunsSuccess()
        {
            // Arrange
            var customerRequest = new CustomerRequest(null, "John", "doe", "johndoe@gmail.com");

            // Act
            var result = CustomerRequestValidator.ValidateAddCustomerRequest(customerRequest);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void ValidateAsync_ValidUpdateCustomerRequest_RetrunsSuccess()
        {
            // Arrange
            var customerId = Guid.NewGuid().ToString();
            var customerRequest = new CustomerRequest(customerId, "John", "doe", "johndoe@gmail.com");

            // Act
            var result = CustomerRequestValidator.ValidateUpdateCustomerRequest(customerRequest);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(InvalidAddCustomerRequest))]
        public void ValidateAsync_ValideAddCustomerRequest_RetrunsFailure(CustomerRequest request)
        {

            // Arrange && Act
            var result = CustomerRequestValidator.ValidateAddCustomerRequest(request);

            // Assert
            result.IsFailure.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(InvalidUpdateCustomerRequest))]
        public void ValidateAsync_ValideUpdateCustomerRequest_RetrunsFailure(CustomerRequest request)
        {
            // Arrange && Act
            var result = CustomerRequestValidator.ValidateAddCustomerRequest(request);

            // Assert
            result.IsFailure.Should().BeTrue();
        }

        public static IEnumerable<object[]> InvalidAddCustomerRequest => new List<object[]>
        {
            new object[] { new CustomerRequest(null, null, "doe", "johndoe@gmail.com") },
            new object[] {new CustomerRequest("test_id", "john", null, "johndoe@gmail.com") },
            new object[] {new CustomerRequest(Guid.NewGuid().ToString(), "john", "doe", null) },
            new object[] {new CustomerRequest(Guid.NewGuid().ToString(), "john", "doe", "johndoe@gmail") },
             new object[] {new CustomerRequest(Guid.NewGuid().ToString(), "Those writing novels or other books may need to come up with various character names.", "doe", "johndoe@gmail.com") },
        };

        public static IEnumerable<object[]> InvalidUpdateCustomerRequest => new List<object[]>
        {
            new object[] {new CustomerRequest(null, null, "doe", "johndoe@gmail.com") },
            new object[] {new CustomerRequest(Guid.NewGuid().ToString(), "john", null, "johndoe@gmail.com") },
            new object[] {new CustomerRequest(Guid.NewGuid().ToString(), "john", "doe", null) },
            new object[] {new CustomerRequest(Guid.NewGuid().ToString(), "john", "doe", "johndoe@gmail") },
             new object[] {new CustomerRequest(Guid.NewGuid().ToString(), "Those writing novels or other books may need to come up with various character names.", "doe", "johndoe@gmail.com") },
        };
    }
}
