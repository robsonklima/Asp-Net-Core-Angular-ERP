using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;

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
                                .UseContentRoot(Directory.GetCurrentDirectory())
                                .UseIISIntegration()
                                .UseStartup<Startup>();
                        })
                        .Build();

            host.Run();
        }
    }
}
