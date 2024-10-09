using Customers.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Customers.Infrastructure.Database
{

    /// <summary>
    /// Represents the database context for the Customers using Entity Framework.
    /// </summary>
    public class CustomersDbContext : DbContext
    {
        /// <summary>
        /// Configures the database options.
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseInMemoryDatabase(databaseName: "customers");

        /// <summary>
        /// Customer entities in the database.
        /// </summary>
        public DbSet<Customer> Customers { get; set; }
    }
}