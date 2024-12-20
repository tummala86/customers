﻿using Customers.API.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Diagnostics;
using System.Net;

namespace Customers.API.Util
{
    public class ProblemDetailsHelper
    {
        public static ProblemDetails InvalidParameters(IDictionary<string, string[]> errors, string instance)
          => new ValidationProblemDetails(errors)
          {
              Type = ErrorMessages.InvalidParametersType,
              Title = ErrorMessages.InvalidParameters,
              Detail = ErrorMessages.InvalidParametersDetail,
              Status = (int)HttpStatusCode.BadRequest,
              Instance = instance,
              Extensions = { { ApiHeaders.TraceId, Activity.Current?.TraceId.ToString() ?? Guid.NewGuid().ToString() } }
          };

        public static ProblemDetails InternalServerError(string instance)
           => CreateProblemDetails(
                ErrorMessages.InternalServerErrorType,
                ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.InternalServerError),
                ErrorMessages.InternalServerErrorDetail,
                (int)HttpStatusCode.InternalServerError,
                instance,
                null
                );

        public static ProblemDetails CustomerNotFound(string instance)
           => CreateProblemDetails(
                ErrorMessages.CustomerNotFoundType,
                ErrorMessages.CustomerNotFound,
                ErrorMessages.CustomerNotFoundErrorDetail,
                (int)HttpStatusCode.NotFound,
                instance,
                null
                );

        private static ProblemDetails CreateProblemDetails(
            string type,
            string title,
            string detail,
            int status,
            string? instance,
            string? traceId)
        => new()
        {
            Type = type,
            Title = title,
            Detail = detail,
            Status = status,
            Instance = instance,
            Extensions = { { ApiHeaders.TraceId, traceId ?? Guid.NewGuid().ToString() } }
        };
    }
}
