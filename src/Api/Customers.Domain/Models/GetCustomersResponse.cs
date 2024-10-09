using OneOf;

namespace Customers.Domain.Models;

[GenerateOneOf]
public partial class GetCustomersResponse : OneOfBase<GetCustomersResponse.Success,
        GetCustomersResponse.InternalError>
{
    public record Success(IEnumerable<Customer>? Customers);

    public record InternalError(string ErrorMessage);

    public bool IsSuccess => IsT0;

    public bool IsInternalError => IsT1;
}
