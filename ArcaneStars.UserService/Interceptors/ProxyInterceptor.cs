using AspectCore.DynamicProxy;
using Newtonsoft.Json;
using NLog;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArcaneStars.Service.Interceptors
{
    public class ProxyInterceptor : AbstractInterceptorAttribute
    {
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            var parameters = JsonConvert.SerializeObject(context.Parameters);

            Log.Information($"Call api {context.ServiceMethod.DeclaringType.FullName}->{context.ServiceMethod.Name} Paramters：{parameters}");

            await next(context);

            if (context.IsAsync())
            {
                if (context.ServiceMethod.ReturnType.FullName == "System.Threading.Tasks.Task")
                {
                    Log.Information($"Result value: void");
                }
                else
                {
                    var result = await context.UnwrapAsyncReturnValue();
                    var res = JsonConvert.SerializeObject(result);
                    Log.Information($"Result value: {res}");
                }
            }
            else
            {
                if (context.ReturnValue != null)
                {
                    var res = JsonConvert.SerializeObject(context.ReturnValue);
                    Log.Information($"Result value: {res}");
                }
            }
        }
    }
}
