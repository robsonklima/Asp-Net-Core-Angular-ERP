using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
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
            string connString = Configuration.GetConnectionString(Constants.DB_PROD);
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connString));
            

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
                cronExpression: Constants.CRON_EVERY_1_MIN));
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
                await context.Response.WriteAsync("SAT.TASKS");
            });
        }
    }
}