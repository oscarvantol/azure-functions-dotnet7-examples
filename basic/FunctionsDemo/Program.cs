using FunctionsDemo.Middleware;
using FunctionsDemo.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(s =>
    {
        
    })
    .Build();

host.Run();



/*
  s.AddSingleton<ISomeService, SomeService>();
  s.AddOptions<ExampleConfig>().Configure<IConfiguration>((s, c) => c.GetSection("ExampleConfig").Bind(s));
*/


/*

b =>
{
  b.UseMiddleware<ExampleMiddleware>();
}

  b.UseWhen<ExceptionSuppressorHttpMiddleware>((context) =>
    context.FunctionDefinition.InputBindings.Values.First(a => a.Type.EndsWith("Trigger")).Type == "httpTrigger");

*/

