using Restaurant.Domain.Exceptions;

namespace Restaurant.API.Middlewares;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> _logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
		try
		{
		await	next.Invoke(context);
		}
		catch (NotFoundException notFound)
		{
            _logger.LogWarning($"404 Error: {notFound.Message}");
            context.Response.StatusCode = 404;
			await context.Response.WriteAsync(notFound.Message);

			_logger.LogWarning(notFound.Message);
		}
		catch (Exception ex)
		{

			_logger.LogError(ex, ex.Message);

			context.Response.StatusCode = 500;
			await context.Response.WriteAsync("SomeThing went wrong");
			
		}
    }
}
