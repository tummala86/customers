using OneOf;

namespace Customers.WebApp.Domain.Models;

[GenerateOneOf]
public partial class GetCustomersResponse : OneOfBase<GetCustomersResponse.Success,
        GetCustomersResponse.Error>
{
    public record Success(IEnumerable<Customer>? Customers);

    public record Error(string ErrorMessage);

    public bool IsSuccess => IsT0;

    public bool IsError => IsT1;
}
