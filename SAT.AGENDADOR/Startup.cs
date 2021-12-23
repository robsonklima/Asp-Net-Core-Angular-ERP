using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.INFRA.Repository;
using SAT.SERVICES.Interfaces;
using SAT.SERVICES.Services;
using System;
using System.Configuration;

namespace SAT.AGENDADOR
{
    public class Startup
    {
        private IIndicadorService _indicadorService;

        public IIndicadorService IndicadorService { get { return this._indicadorService; } }

        public Startup()
        {
            this.ConfigureServices();
        }

        public void ConfigureServices()
        {
            try
            {
                IServiceCollection services = new ServiceCollection();

                // Configura o DB
                services.AddDbContext<AppDbContext>(options => options.UseSqlServer(ConfigurationManager.AppSettings["Prod"]));

                /** Inicializa os reposit�rios e servi�os **/

                // Reposit�rios
                services.AddTransient<ITecnicoRepository, TecnicoRepository>();
                services.AddTransient<IFeriadoRepository, FeriadoRepository>();
                services.AddTransient<IDashboardRepository, DashboardRepository>();
                services.AddTransient<IOrdemServicoRepository, OrdemServicoRepository>();
                services.AddTransient<IEquipamentoContratoRepository, EquipamentoContratoRepository>();
                services.AddTransient<IDispBBRegiaoFilialRepository, DispBBRegiaoFilialRepository>();
                services.AddTransient<IDispBBCriticidadeRepository, DispBBCriticidadeRepository>();
                services.AddTransient<IDispBBPercRegiaoRepository, DispBBPercRegiaoRepository>();
                services.AddTransient<IDispBBDesvioRepository, DispBBDesvioRepository>();

                // Servi�os
                services.AddTransient<IFeriadoService, FeriadoService>();
                services.AddTransient<IDashboardService, DashboardService>();
                services.AddTransient<IIndicadorService, IndicadorService>();

                ServiceProvider serviceProvider = services.BuildServiceProvider();

                // Inicializa os servi�os que ser�o usados
                this._indicadorService = serviceProvider.GetService<IIndicadorService>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}