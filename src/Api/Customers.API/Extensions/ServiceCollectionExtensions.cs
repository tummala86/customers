using Customers.Domain.Handlers;
using Customers.Domain.Ports;
using Customers.Infrastructure;
using Customers.Infrastructure.Ports;
using Customers.Infrastructure.Repositories;

namespace Customers.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void SetupServices(this IServiceCollection services, IConfiguration configuration)
        {

            // Domain services
            services.AddTransient<ICustomerCommandHandler, CustomerCommandHandler>();
            services.AddTransient<IGetCustomersQueryHandler, GetCustomersHandler>();

            // Infrastructure services
            services.AddTransient<ICustomerCommand, CustomersCommand>();
            services.AddTransient<IGetCustomersQuery, GetCustomersQuery>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
        }
    }
}
