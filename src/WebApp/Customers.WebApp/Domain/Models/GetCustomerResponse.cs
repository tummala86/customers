using OneOf;

namespace Customers.WebApp.Domain.Models
{
    [GenerateOneOf]
    public partial class GetCustomerResponse : OneOfBase<GetCustomerResponse.Success,
          GetCustomerResponse.Error>
    {
        public record Success(Customer Customer);

        public record Error(string ErrorMessage);

        public bool IsSuccess => IsT0;
        public Success AsSuccess => AsT0;

        public bool IsError => IsT1;
        public Error AsError => AsT1;
    }
}