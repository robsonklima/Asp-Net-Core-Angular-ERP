using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.SERVICES.Interfaces;
using SAT.SERVICES.Services;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SAT.API.Support
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private static IEmailService _emailService;

        public CustomExceptionMiddleware(
            RequestDelegate next,
            IEmailService emailService
        )
        {
            _next = next;
            _emailService = emailService;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex).ConfigureAwait(false);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(new { statusCode = statusCode, errorMessage = Constants.ERROR });
            LoggerService.LogError($"{statusCode} {ex.Message} {ex.InnerException}");
            _emailService.Enviar(new Email() {
                    Assunto = "Erro durante o uso do SAT.V2",
                    Corpo = $"{statusCode} {ex.Message} {ex.InnerException}",
                    EmailDestinatario = Constants.EQUIPE_SAT_EMAIL,
                    EmailRemetente = Constants.EQUIPE_SAT_EMAIL
                });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}
