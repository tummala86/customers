using Customers.Infrastructure.Database.Entities;
using OneOf;

namespace Customers.Infrastructure.Models
{
    [GenerateOneOf]
    public partial class GetCustomerResponse : OneOfBase<GetCustomerResponse.Success,
        GetCustomerResponse.NotFound,
        GetCustomerResponse.InternalError>
    {
        public record Success(Customer? Customer);
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
