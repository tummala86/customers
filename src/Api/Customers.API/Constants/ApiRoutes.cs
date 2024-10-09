namespace Customers.API.Constants
{
    public static class ApiRoutes
    {
        // API Version standards
        public const string Version = "v3";

        public const string Base = Version+ "/customers";

        public static class HealthChecks
        {
            public const string Internal = "/up";
        }
    }
}