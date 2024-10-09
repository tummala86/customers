namespace Customers.Domain.Validators
{
    public record ParameterError(string ParameterName, string ErrorMessage);

    public static class StandardParameterErrors
    {
        public static ParameterError RequestBodyRequired(string fieldName) => new(fieldName, "Request boday is required.");

        public static ParameterError Required(string fieldName) => new(fieldName, "Value is required.");

        public static ParameterError InvalidValueLength(string fieldName, int allowedLength)
            => new(fieldName, $"Value provided cannot be longer than {allowedLength} characters.");

        public static ParameterError InvalidUuidValue(string fieldName)
            => new(fieldName, "Value provided doesn't match a valid uuid.");

        public static ParameterError InvalidEmailId(string fieldName)
            => new(fieldName, $"Value provided doesn't match a valid email id.");
    }

    public static class ParameterErrorExtensions
    {
        public static ParameterError WithPrefix(this ParameterError parameterError, string prefix)
            => new(string.IsNullOrEmpty(parameterError.ParameterName) ? prefix : $"{prefix}.{parameterError.ParameterName}", parameterError.ErrorMessage);


        public static Dictionary<string, string[]> GetGroupedErrors(this IEnumerable<ParameterError> errors)
            => errors.GroupBy(x => x.ParameterName)
                .ToDictionary(
                   x => x.Key,
                   x => x.Select(e => e.ErrorMessage).ToArray());
    }
}
