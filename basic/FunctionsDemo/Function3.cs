using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace FunctionsDemo;

public class Function3
{
    private readonly ILogger _logger;

    public Function3(ILogger<Function3> logger)
    {
        _logger = logger;
    }

    [Function("Function3")]
    public HttpResponseData Runa([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.WriteString("Welcome to Azure Functions!");
        return response;
    }

}



//Multi output HttpResponseData /  [BlobOutput("function3/multioutput.txt")]
