using FunctionsDemo.Middleware;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults(b =>
    {
        b.UseMiddleware<ExampleMiddleware>();
        /*
         * b.UseWhen<ExampleMiddleware>((context) =>
            context.FunctionDefinition.InputBindings.Values.First(a => a.Type.EndsWith("Trigger")).Type == "httpTrigger");
        */
    })
    .ConfigureServices(s =>
    {
        //s.AddSingleton<ISomeService, SomeService>();
        // s.AddOptions<ExampleConfig>().Configure<IConfiguration>((s, c) => c.GetSection("ExampleConfig").Bind(s));

    })
    .Build();

host.Run();
