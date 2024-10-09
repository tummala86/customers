using Customers.API.Models;
using Customers.Domain.Validators;
using ValidationResult = Customers.Domain.Validators.ValidationResult;

namespace Customers.API.Validation
{
    public static class CustomerRequestValidator
    {
        public static ValidationResult ValidateAddCustomerRequest(CustomerRequest? request)
        {
            if (request is null)
            {
                return StandardParameterErrors.RequestBodyRequired("$");
            }

            return ValidateRequest(request);
        }

        public static ValidationResult ValidateUpdateCustomerRequest(CustomerRequest? request)
        {
            if (request is null)
            {
                return StandardParameterErrors.RequestBodyRequired("$");
            }

            return StandardValidators.ValidateRequired(nameof(request.Id), request.Id)
            .ContinueIfSuccess(() => StandardValidators.ValidateUuid(nameof(request.Id), request.Id))
            .ContinueIfSuccess(() => ValidateRequest(request));
        }

        private static ValidationResult ValidateRequest(CustomerRequest? request)
        {
            return StandardValidators.ValidateRequired(nameof(request.FirstName), request.FirstName)
                .ContinueIfSuccess(() => StandardValidators.ValidateFieldLenght(nameof(request.FirstName), request.FirstName, 50))
                .ContinueIfSuccess(() => StandardValidators.ValidateRequired(nameof(request.LastName), request.LastName))
                .ContinueIfSuccess(() => StandardValidators.ValidateFieldLenght(nameof(request.LastName), request.LastName, 50))
                .ContinueIfSuccess(() => StandardValidators.ValidateRequired(nameof(request.Email), request.Email)
                .ContinueIfSuccess(() => StandardValidators.ValidateEmailId(nameof(request.Email), request.Email!)));
        }
    }
}
