using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionsDemo;

public class Function2
{
    private readonly ILogger _logger;

    public Function2(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<Function2>();
    }

    [Function("Function2")]
    [BlobOutput("function2/lastrun.txt")]
    public string Run([TimerTrigger("0 */5 * * * *", RunOnStartup = true)] TimerInfo myTimer)
    {
        _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus?.Next}");
        return $"Last run: {DateTime.Now}";
    }
}
