using Microsoft.AspNetCore.Builder;
using System.Net;

namespace storytiling.api.Helpers.CustomExceptions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
        public record ExceptionResponse(HttpStatusCode StatusCode, string Description);
    }

}
