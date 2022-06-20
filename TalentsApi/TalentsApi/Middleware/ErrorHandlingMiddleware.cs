using TalentsApi.Exceptions;
using Newtonsoft.Json;
using TalentsApi.Models.Excetpions;

namespace TalentsApi.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (ForbidException forbidException)
            {
                context.Response.StatusCode = 403;
            }
            catch (BadRequestException badRequestException)
            {
                ExceptionModel exModel = new ExceptionModel()
                {
                    ErrorMessage = badRequestException.Message
                };
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(exModel);
            }
            catch (NotFoundException notFoundException)
            {
                ExceptionModel exModel = new ExceptionModel()
                {
                    ErrorMessage = notFoundException.Message
                };
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(exModel);
            }
            catch (Exception e)
            {
                ExceptionModel exModel;

                _logger.LogError(e, e.Message);
                if(isDevelopment)
                {
                    exModel = new ExceptionModel()
                    {
                        ErrorMessage = e.Message,
                        InnerMessage = e.InnerException?.ToString()
                    };
                }
                else
                {
                    exModel = new ExceptionModel()
                    {
                        ErrorMessage = "Something gone wrong!"
                    };
                }

                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(exModel);
            }
        }
    }
}
