using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Filters
{
    public class LoggingFilter:IActionFilter
    {
        private ILogger _logger;

        public LoggingFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<LoggingFilter>();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionDescriptor is ControllerActionDescriptor actionDescriptor)
            {
                _logger.LogDebug($"{actionDescriptor.ControllerName}.{actionDescriptor.ActionName} initiated");
            }
            else
            {
                _logger.LogDebug($"ActionDescriptor is not an ControllerActionDescriptor");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.ActionDescriptor is ControllerActionDescriptor actionDescriptor)
            {
                _logger.LogDebug($"{actionDescriptor.ControllerName}.{actionDescriptor.ActionName} completed");
            }
            else
            {
                _logger.LogDebug($"ActionDescriptor is not an ControllerActionDescriptor");
            }
        }
    }
}
