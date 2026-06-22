namespace Day1Task1.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var start = DateTime.Now;

        await _next(context);

        var elapsed = DateTime.Now - start;

        Console.WriteLine(
            $"[{context.Request.Method}] " +
            $"{context.Request.Path} " +
            $"Status: {context.Response.StatusCode} " +
            $"Time: {elapsed.TotalMilliseconds} ms");
    }
}