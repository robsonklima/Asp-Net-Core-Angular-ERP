using SAT.INFRA.Context;
using Microsoft.EntityFrameworkCore;
using Autofac;
using SAT.IOC;
using Autofac.Extensions.DependencyInjection;
using SAT.TASKS;
using SAT.MODELS.Entities.Constants;

using IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "SAT.TASKS";
    })
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer(context.Configuration.GetConnectionString(Constants.DB_PROD)!,
                sqlServerOptions => sqlServerOptions.CommandTimeout(30)));

        services.AddHttpContextAccessor();
        services.AddHostedService<Worker>();
    })
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new ModuleIOC()))
    .Build();

await host.RunAsync();