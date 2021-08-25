using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Albelli.OrderManagement.Api.Middlewares
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate del;

        public ExceptionHandler(RequestDelegate del)
        {
            this.del = del;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await del(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var message = "Unexpected error";
            var description = "Unexpected error";

            if (exception != null)
            {
                message = exception.Message;
                description = exception.StackTrace;
            }

            response.ContentType = "application/json";
            response.StatusCode = statusCode;
            await response.WriteAsync(JsonConvert.SerializeObject(new
            {
                Message = message
            }));
        }
    }
}
