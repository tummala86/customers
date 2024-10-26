using O9d.Json.Formatting;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Customers.Test.Integration.Utils
{
    public static class Serilization
    {
        public static JsonSerializerOptions JsonSerializerOptions { get; } = new(JsonSerializerDefaults.Web)
        {
            PropertyNamingPolicy = new SnakeCaseNamingPolicy(),
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters = { new JsonStringEnumConverter(new SnakeCaseNamingPolicy()) },
        };

        public class SnakeCaseNamingPolicy : JsonNamingPolicy
        {
            public override string ConvertName(string name) => name.ToSnakeCase();
        }
    }
}
