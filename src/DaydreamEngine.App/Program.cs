using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => { services.AddTransient<MyService>(); })
    .Build();

var my = host.Services.GetRequiredService<MyService>();
await my.ExecuteAsync();
