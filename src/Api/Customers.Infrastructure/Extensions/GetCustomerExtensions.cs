using DomainCustomer= Customers.Domain.Models.Customer;
using Customers.Infrastructure.Database.Entities;

namespace Customers.Infrastructure.Extensions
{
    public static class GetCustomerExtensions
    {
        public static DomainCustomer ToDomainCustomer(this Customer customer) => 
            new(customer.Id, customer.FirstName,customer.LastName,customer.Email);

        public static IEnumerable<DomainCustomer> ToDomainCustomers(this IEnumerable<Customer> customers) =>
            customers.Select(t => new DomainCustomer(t.Id, t.FirstName, t.LastName, t.Email)).ToList();
    }
}
