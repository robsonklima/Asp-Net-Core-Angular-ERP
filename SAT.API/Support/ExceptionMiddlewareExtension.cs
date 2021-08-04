using Microsoft.AspNetCore.Builder;

namespace SAT.API.Support
{
    public static class ExceptionMiddlewareExtension
    {
        public static void UseCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
