using SAT.INFRA.Context;
using Microsoft.EntityFrameworkCore;
using Autofac.Extensions.DependencyInjection;
using SAT.TASKS;
using Microsoft.AspNetCore.Builder;
using SAT.MODELS.Entities.Constants;

using IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "SAT.TASKS";
    })
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer(context.Configuration.GetConnectionString(Constants.DB_PROD),
                sqlServerOptions => sqlServerOptions.CommandTimeout(180)));

        services.AddHttpContextAccessor();
        services.AddMvc();
        services.AddSession();

        services.Configure<IISOptions>(o =>
        {
            o.ForwardClientCertificate = false;
        });

        services.AddHostedService<Worker>();
    })
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureWebHostDefaults(webHostBuilder => {
        webHostBuilder
            .UseStartup<Startup>();
    })
    .Build();

await host.RunAsync();