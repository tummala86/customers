using Customers.API.Constants;
using Customers.API.Extensions;
using Customers.API.Middleware;
using Hellang.Middleware.ProblemDetails;
using O9d.Json.Formatting;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.SetupServices(builder.Configuration);
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = new JsonSnakeCaseNamingPolicy();
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(new JsonSnakeCaseNamingPolicy()));
    });
builder.Services.AddProblemDetails(Customers.API.Extensions.ProblemDetailsExtensions.ConfigureProblemDetails);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddDistributedMemoryCache();



var app = builder.Build();

app.UseRouting();

app.UseProblemDetails();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<TraceIdMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHealthChecks(ApiRoutes.HealthChecks.Internal);
});

app.Run();

namespace Customers.API
{
    public partial class Program { }
}
