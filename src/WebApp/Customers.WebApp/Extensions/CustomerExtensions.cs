using Customers.WebApp.Domain.Models;
using Customers.WebApp.Models;

namespace Customers.WebApp.Extensions
{
    public static class CustomerExtensions
    {
        public static IEnumerable<CustomerModel> ToCustomersViewModel(this IEnumerable<Customer> customers)
        {
            return customers.Select(c => new CustomerModel() {
                Id = c.CustomerId,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email
            }).ToList();
        }

        public static CustomerModel ToCustomerViewModel(this Customer customer)
        {
            return new CustomerModel()
            {
                Id = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email
            };
        }

        public static Customer ToDomainCustomer(this CustomerModel customer)
        {
            return new Customer(customer.Id, customer.FirstName, customer.LastName, customer.Email);
        }
     }
}
