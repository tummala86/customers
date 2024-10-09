namespace Customers.Domain.Models;

public record Customer(Guid CustomerId, string FirstName, string LastName, string Email);
