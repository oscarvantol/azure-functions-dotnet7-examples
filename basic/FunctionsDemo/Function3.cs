using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace FunctionsDemo;

public class Function3
{
    private readonly ILogger _logger;

    public Function3(ILogger<Function3> logger)
    {
        _logger = logger;
    }

    [Function("Function3a")]
    public string Runa([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
    {
        _logger.LogWarning("Also works!");

        return "Bye!";
    }

}

//add Runb, jsonResult, person record
//add IOptions ExampleConfig
//add Runc, Multi output HttpResponseData /  [BlobOutput("function3c/multioutput.txt")]
