using FluentValidation;
using GiftSuggester.Core.Exceptions;

namespace GiftSuggester.Web.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (EntityNotFoundException entityNotFoundException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            await httpContext.Response.WriteAsJsonAsync(new { Error = $"{entityNotFoundException.Message}" });
        }
        catch (ValidationException validationException)
        {
            var errorMessage = string.Join(Environment.NewLine, validationException.Errors);

            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            await httpContext.Response.WriteAsJsonAsync(new { Error = $"{errorMessage}" });
        }
        catch (Exception exception)
        {
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(new { Error = $"{exception.Message}" });
        }
    }
}