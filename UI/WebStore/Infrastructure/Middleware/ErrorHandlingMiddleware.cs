using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace WebStore.Infrastructure.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                HanleException(error, context);
                throw;
            }
        }

        private void HanleException(Exception error, HttpContext context)
        {
            _logger.LogError(error, $"Ошибка при обработке запроса к {context.Request.Path}");
        }
    }
}
