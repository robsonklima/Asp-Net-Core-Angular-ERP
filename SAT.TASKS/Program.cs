using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using SAT.TASKS;

namespace SAT.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                        .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                        .ConfigureWebHostDefaults(webHostBuilder => {
                            webHostBuilder
                                .UseStartup<Startup>();
                        })
                        .Build();

            host.Run();
        }
    }
}
