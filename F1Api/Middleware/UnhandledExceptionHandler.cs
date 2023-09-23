namespace F1Api.Middleware;

public class UnhandledExceptionHandler : IMiddleware
{
    private readonly ILogger<UnhandledExceptionHandler> _logger;

    public UnhandledExceptionHandler(ILogger<UnhandledExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try 
        { 
            await next(context); 
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "{message}", ex.Message);
        }
    }
}

public static class UnhandledExceptionHandlerExtension
{
    public static void UseUnhandledExceptionHandler(this IApplicationBuilder app) =>
        app.UseMiddleware<UnhandledExceptionHandler>();
}
