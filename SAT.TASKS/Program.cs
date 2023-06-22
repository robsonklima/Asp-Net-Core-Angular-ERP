using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.MODELS.Entities.Constants;
using SAT.TASKS;

using IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "SAT.V2 TASKS";
    })
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureWebHostDefaults(webHostBuilder => {
        webHostBuilder
            .UseStartup<Startup>();
    })
    .Build();

await host.RunAsync();