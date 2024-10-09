using System.ComponentModel.DataAnnotations;

namespace Customers.Infrastructure.Database.Entities
{
    public class Customer
    {
        [Key]
        public required Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; } = null;
    }
}