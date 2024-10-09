using OneOf;

namespace Customers.WebApp.Domain.Models
{
    [GenerateOneOf]
    public partial class CreateCustomerResponse : OneOfBase<CreateCustomerResponse.Success,
        CreateCustomerResponse.Error>
    {
        public record Success();
        public record Error(string ErrorMessage);

        public bool IsSuccess => IsT0;
        public Success AsSuccess => AsT0;

        public bool IsError => IsT1;
        public Error AsError => AsT1;
    }
}
