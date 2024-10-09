using Customers.API.Models;
using Customers.Domain.Models;

namespace Customers.API.Extensions
{
    public static class CustomersRequestExtensions
    {
        public static Customer ToDomain(this CustomerRequest request)
        => new(request.Id.ToGuid(), request.FirstName, request.LastName, request.Email);

        private static Guid ToGuid(this string? id)
        => string.IsNullOrWhiteSpace(id) ? Guid.NewGuid() : Guid.Parse(id!);
    }
}
