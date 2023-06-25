using System.Net;
using System.Text.Json;
using MMS.Core.Commands.FluentValidation;
using MMS.Service.Common.Exceptions;

namespace MMS.API.MiddleWares
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = exception switch
            {
                ArgumentException => HttpStatusCode.BadRequest,
                CommandValidationException => HttpStatusCode.BadRequest,
                RecommendationException => HttpStatusCode.UnprocessableEntity,
                EntityNotFoundException => HttpStatusCode.NotFound,
                NotImplementedException => HttpStatusCode.NotImplemented,
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                _ => HttpStatusCode.InternalServerError
            };

            var result = JsonSerializer.Serialize(new { error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
