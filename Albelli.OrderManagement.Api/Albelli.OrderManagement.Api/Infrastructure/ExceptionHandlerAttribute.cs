using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Albelli.OrderManagement.Api.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ILogger<ExceptionHandlerAttribute> _logger;

        public ExceptionHandlerAttribute(IWebHostEnvironment hostingEnvironment, ILogger<ExceptionHandlerAttribute> logger)
        {
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Exception has caught");

            context.Result = _hostingEnvironment.IsDevelopment()
                ? new ObjectResult(context.Exception.ToString())
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                }
                : new ObjectResult(context.Exception.Message)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
        }
    }
}