using ArcaneStars.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog;
using Serilog;
using System.Linq;

namespace ArcaneStars.ServiceHost.Filters
{
    public class LogFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Log.Information($"call {context.ActionDescriptor.DisplayName}");

            var ret = context.Result as ObjectResult;
            var objstr = JsonConvert.SerializeObject(ret);

            Log.Information($"{context.ActionDescriptor.DisplayName} result:{objstr}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var parameters = context.ActionArguments;
            var controller = context.Controller.ToString();
            var actionName = context.ActionDescriptor.DisplayName;

            var patams = string.Join(";", parameters.Select(o => string.Join("=", o.Key, o.Value)));
            Log.Information($"parameters:{patams}");
        }
    }
}