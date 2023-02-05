
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;

namespace SimpleDurable;

public class Function1
{
    private ILogger<Function1> _logger;
    private Random _rnd;

    public Function1(ILogger<Function1> logger)
    {
        _logger = logger;
        _rnd = new Random();
    }

    [Function("Function1")]
    public async Task<string> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req, [DurableClient] DurableTaskClient client)
    {
        string instanceId = await client.ScheduleNewOrchestrationInstanceAsync(nameof(MyOrchestration));
        return "ok";
    }


    [Function(nameof(MyOrchestration))]
    public async Task MyOrchestration([OrchestrationTrigger] TaskOrchestrationContext context, string input)
    {
        _logger.LogInformation("-------------------(RE)STARTING Orchestration-----------------------");
        string result = "";
        result += await context.CallActivityAsync<string>(nameof(MyActivity1), "Something ");
        result += await context.CallActivityAsync<string>(nameof(MyActivity1), "Works ");

        //fan out
        var task1 = context.CallActivityAsync<string>(nameof(MyActivity1), "Or ");
        var task2 = context.CallActivityAsync<string>(nameof(MyActivity1), "Maybe ");
        var task3 = context.CallActivityAsync<string>(nameof(MyActivity1), "Not ");

        var results = await Task.WhenAll(task1, task2, task3);
        result += string.Join("", results);


        _logger.LogWarning($"The result is: {result}");
        _logger.LogInformation("-------------------ENDING Orchestration-----------------------");
    }

    [Function(nameof(MyActivity1))]
    public async Task<string> MyActivity1([ActivityTrigger] string input)
    {
        //random delay to simulate IO
        await Task.Delay(TimeSpan.FromSeconds(_rnd.Next(1, 6)));
        _logger.LogWarning($"Activity1 with input: {input}");
        return input;
    }
}
