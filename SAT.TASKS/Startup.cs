using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Autofac;
using SAT.IOC;
using SAT.MODELS.Entities.Constants;

namespace SAT.TASKS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString(Constants.DB_PROD)));

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
                cronExpression: "*/5 * * * * ?")); // Every 5 minutes

            services.AddSingleton(new JobSchedule(
                jobType: typeof(ModeloEquipamentoJob),
                cronExpression: "50 23 * * * ?")); // At 23:50

            services.AddSingleton(new JobSchedule(
                jobType: typeof(IntegracaoMRPJob),
                cronExpression: "00 02 * * * ?")); // At 02:00

            services.AddSingleton(new JobSchedule(
                jobType: typeof(IntegracaoBBJob),
                cronExpression: "*/5 * * * * ?")); // Every 5 minutes
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ModuleIOC());
        }

        [Obsolete]
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}