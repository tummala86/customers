using Customers.Infrastructure.Database.Entities;

namespace Customers.Infrastructure.Extensions
{
    public static class CustomerExtensions
    {
        public static Customer ToCustomerDbEntity(this Domain.Models.Customer customer) => new()
            {
               Id = customer.CustomerId,
               FirstName = customer.FirstName,
               LastName = customer.LastName,
               Email= customer.Email,
               IsActive = true,
               CreatedAt = DateTime.UtcNow
            };
    }
}
