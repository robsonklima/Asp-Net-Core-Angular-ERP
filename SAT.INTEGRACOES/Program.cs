using System;
using System.Configuration;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Repository;
using SAT.INTEGRACOES.Interfaces;

namespace SAT.INTEGRACOES
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDbContext __dbContext = new AppDbContext
                   (new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(ConfigurationManager.AppSettings["Homolog"]).Options);

            AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                 .Where(x => typeof(IIntegracao).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).AsParallel().ForAll(e =>
               {
                   IIntegracao integracaoInstance = (IIntegracao)Activator.CreateInstance(e);
                   ((BaseIntegracao)integracaoInstance).InjectDbContext(__dbContext);
                   ((BaseIntegracao)integracaoInstance).InjectEmailService(new SERVICES.Services.EmailService());
                   ((BaseIntegracao)integracaoInstance).Initialize();
               });
        }
    }
}