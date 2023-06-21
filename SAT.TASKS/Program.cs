// 
using SAT.INFRA.Context;
using Microsoft.EntityFrameworkCore;
using Autofac;
using SAT.IOC;
using Autofac.Extensions.DependencyInjection;
using SAT.TASKS;
using SAT.MODELS.Entities.Constants;
using Quartz.Spi;
using Quartz;
using Quartz.Impl;

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

        services.AddSingleton<IJobFactory, JobFactory>();
        services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
        services.AddSingleton<QuartzJobRunner>();
        services.AddHostedService<QuartzHostedService>();

        // Jobs Initializing
        services.AddScoped<IntegracaoBanrisulJob>();
        services.AddScoped<ModeloEquipamentoJob>();
        services.AddScoped<IntegracaoMRPJob>();
        services.AddScoped<IntegracaoBBJob>();

        // Jogs Schedules
        services.AddSingleton(new JobSchedule(
            jobType: typeof(IntegracaoBanrisulJob),
            cronExpression: Constants.CRON_EVERY_5_MIN));

        services.AddSingleton(new JobSchedule(
            jobType: typeof(ModeloEquipamentoJob),
            cronExpression: Constants.CRON_EVERY_23_50 ));

        services.AddSingleton(new JobSchedule(
            jobType: typeof(IntegracaoMRPJob),
            cronExpression: Constants.CRON_EVERY_02_00 ));

        services.AddSingleton(new JobSchedule(
            jobType: typeof(IntegracaoBBJob),
            cronExpression: Constants.CRON_EVERY_5_MIN));
    })
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new ModuleIOC()))
    .Build();

host.Run();