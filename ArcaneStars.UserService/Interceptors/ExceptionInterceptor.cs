using AspectCore.DynamicProxy;
using Microsoft.Extensions.Logging;
using NLog;
using Serilog;
using System;
using System.Threading.Tasks;

namespace ArcaneStars.Service.Interceptors
{
    public class ExceptionInterceptor : AbstractInterceptorAttribute
    {
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                throw;
            }
        }
    }
}
