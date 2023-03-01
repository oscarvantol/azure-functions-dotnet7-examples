using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;

namespace FunctionsDemo.Middleware;

public class ExceptionSuppressorHttpMiddleware : IFunctionsWorkerMiddleware
{
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        try
        {
            await next(context);
        }
        catch
        {
            var httpReqData = await context.GetHttpRequestDataAsync();
            if (httpReqData != null)
            {
                var newHttpResponse = httpReqData.CreateResponse();
                await newHttpResponse.WriteAsJsonAsync(new { FooStatus = "Invocation failed!" });

                var invocationResult = context.GetInvocationResult();
                invocationResult.Value = newHttpResponse;
            }
        }
    }
}
