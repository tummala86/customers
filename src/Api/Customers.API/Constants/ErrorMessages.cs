namespace Customers.API.Constants
{
    public static class ErrorMessages
    {
        public const string InvalidParameters = "Invalid parameters";
        public const string InvalidParametersDetail = "The request was invalid";
        public const string InvalidParametersType = "invalid-parameters";

        public const string InternalServerErrorDetail = "Something went wrong, please try again later";
        public const string InternalServerErrorType = "internal-server-error";
        public const string InternalServerError = "Internal server error";

        public const string CustomerNotFoundErrorDetail = "Customer Id must exists";
        public const string CustomerNotFoundType = "customer-not-found";
        public const string CustomerNotFound = "Customer details not found";
    }
}
