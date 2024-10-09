using OneOf;

namespace Customers.Domain.Models
{
    [GenerateOneOf]
    public partial class UpdateCustomerResponse : OneOfBase<UpdateCustomerResponse.Success,
        UpdateCustomerResponse.NotFound,
        UpdateCustomerResponse.InternalError>
    {
        public record Success(Customer Customer);
        public record NotFound();
        public record InternalError(string ErrorMessage);

        public bool IsSuccess => IsT0;
        public Success AsSuccess => AsT0;

        public bool IsNotFound => IsT1;
        public NotFound AsNotFound => AsT1;

        public bool IsInternalError => IsT2;
        public InternalError AsInternalError => AsT2;
    }
}
