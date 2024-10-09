using Customers.Domain.Validators;

namespace Customers.API.Validation
{
    public static class GetCustomerRequestValidator
    {
        public static ValidationResult Validate(string? customerId)
            => StandardValidators.ValidateRequired(nameof(customerId), customerId)
            .ContinueIfSuccess(() => StandardValidators.ValidateUuid(nameof(customerId), customerId));
    }
}
