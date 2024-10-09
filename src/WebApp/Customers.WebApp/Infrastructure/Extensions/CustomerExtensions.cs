using Customers.WebApp.Domain.Models;
using Customers.WebApp.Infrastructure.Models;
using Customers.WebApp.Models;

namespace Customers.WebApp.Infrastructure.Extensions
{
    public static class CustomerExtensions
    {
        public static IEnumerable<Customer> ToDomain(this IEnumerable<CustomerResponse> customers)
        {
            return customers.Select(c => new Customer(c.Id, c.FirstName, c.LastName, c.Email)).ToList();
        }

        public static Customer ToDomain(this CustomerResponse customer)
        {
            return new Customer(customer.Id, customer.FirstName, customer.LastName, customer.Email);
        }
     }
}
