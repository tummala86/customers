using System.Text.Json.Serialization;

namespace Customers.WebApp.Infrastructure.Models;

public class CustomerRequest
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }
    [JsonPropertyName("last_name")]
    public string LastName { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }
}
