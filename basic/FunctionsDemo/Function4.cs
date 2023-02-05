using Microsoft.Azure.Functions.Worker;

namespace FunctionsDemo;

public class Function4
{
    [Function(nameof(Warmup))]
    public void Warmup([WarmupTrigger()] object warmupContext)
    {
        //warmup is only supported in premium plan
    }
}
