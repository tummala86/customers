using Customers.Infrastructure.Database.Entities;
using OneOf;

namespace Customers.Infrastructure.Models
{
    [GenerateOneOf]
    public partial class GetCustomersResponse : OneOfBase<GetCustomersResponse.Success,
        GetCustomersResponse.InternalError>
    {
        public record Success(IEnumerable<Customer?> Customers);
        public record InternalError(string ErrorMessage);

        public bool IsSuccess => IsT0;
        public Success AsSuccess => AsT0;

        public bool IsInternalError => IsT1;
        public InternalError AsInternalError => AsT1;
    }
}
