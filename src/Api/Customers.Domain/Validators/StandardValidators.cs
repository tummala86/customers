using System.Text.RegularExpressions;

namespace Customers.Domain.Validators
{
    public static class StandardValidators
    {
        public static ValidationResult ValidateRequired(string fieldName, string? value)
            => string.IsNullOrWhiteSpace(value)
            ? StandardParameterErrors.Required(fieldName)
            : new ValidationResult.Success();

        public static ValidationResult ValidateUuid(string fieldName, string? value)
            => !Guid.TryParse(value, out var uuid)
            ? StandardParameterErrors.InvalidUuidValue(fieldName)
            : new ValidationResult.Success();

        public static ValidationResult ValidateFieldLenght(string fieldName, string? value, int allowedLimit)
            => !string.IsNullOrWhiteSpace(value) && value.Length > allowedLimit
            ? StandardParameterErrors.InvalidValueLength(fieldName,allowedLimit)
            : new ValidationResult.Success();

        public static ValidationResult ValidateEmailId(string fieldName, string value)
        {
            Regex regex = new("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
            return !regex.Match(value).Success
                           ? StandardParameterErrors.InvalidEmailId(fieldName)
                           : new ValidationResult.Success();
        }
    }
}
