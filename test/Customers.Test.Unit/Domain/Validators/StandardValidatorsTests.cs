using Customers.Domain.Validators;
using FluentAssertions;
using Xunit;

namespace Customers.API.Test.Unit.Domain.Validators
{
    public class StandardValidatorsTests
    {
        [Theory]
        [InlineData("field1", "A-value", true)]
        [InlineData("field1", null, false)]
        public void Should_Validate_Required_Field(string fieldName, string? value, bool isValid)
        {
            StandardValidators.ValidateRequired(fieldName, value).IsSuccess.Should().Be(isValid);
        }

        [Theory]
        [InlineData("field1", "07b1fed9-f612-4d04-bd96-ab92e551de19", true)]
        [InlineData("field1", "test", false)]
        public void Should_Validate_Uuid_Field(string fieldName, string? value, bool isValid)
        {
            StandardValidators.ValidateUuid(fieldName, value).IsSuccess.Should().Be(isValid);
        }

        [Theory]
        [InlineData("FirstName", "JohnJohnJohn",5, false)]
        [InlineData("FirstName", "John",5, true)]
        public void Should_Validate_FieldLength(string fieldName, string? value,int allowedLength, bool isValid)
        {
            StandardValidators.ValidateFieldLenght(fieldName, value, allowedLength).IsSuccess.Should().Be(isValid);
        }

        [Theory]
        [InlineData("EmailId", "test@gmail.com", true)]
        [InlineData("EmailId", "test", false)]
        [InlineData("EmailId", "", false)]
        public void Should_Validate_EmailId_Field(string fieldName, string? value, bool isValid)
        {
            StandardValidators.ValidateEmailId(fieldName, value).IsSuccess.Should().Be(isValid);
        }
    }
}
