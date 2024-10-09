using Customers.WebApp.Domain;
using Customers.WebApp.Infrastructure;
using O9d.Json.Formatting;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = new JsonSnakeCaseNamingPolicy();
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(new JsonSnakeCaseNamingPolicy()));
    });

builder.Services.AddHttpClient<ICustomerClient, CustomerClient>("CustomersApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5239/");
});

builder.Services.AddScoped<ICustomerHandler, CustomerHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
         name: "default",
         pattern: "{controller=Customer}/{action=Index}");

app.Run();
