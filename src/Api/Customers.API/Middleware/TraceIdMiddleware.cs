using Customers.API.Constants;
using System.Diagnostics;

namespace Customers.API.Middleware;

public class TraceIdMiddleware
{
    private readonly RequestDelegate _next;

    public TraceIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var traceId = Activity.Current?.TraceId;
        context.Response.Headers.Add(ApiHeaders.TraceId, traceId?.ToString());

        await _next(context);
    }
}

