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
        private IAgendaTecnicoService _agendaTecnicoService;

        public IIndicadorService IndicadorService { get { return this._indicadorService; } }
        public IAgendaTecnicoService AgendaTecnicoService { get { return this._agendaTecnicoService; } }

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
#if DEBUG
                services.AddDbContext<AppDbContext>(options => options.UseSqlServer(ConfigurationManager.AppSettings["Homolog"]));
#else
                services.AddDbContext<AppDbContext>(options => options.UseSqlServer(ConfigurationManager.AppSettings["Prod"]));
#endif

                /** Inicializa os repositorios e servicos **/

                // Repositorios
                services.AddTransient<ITecnicoRepository, TecnicoRepository>();
                services.AddTransient<IFeriadoRepository, FeriadoRepository>();
                services.AddTransient<IDashboardRepository, DashboardRepository>();
                services.AddTransient<IOrdemServicoRepository, OrdemServicoRepository>();
                services.AddTransient<IEquipamentoContratoRepository, EquipamentoContratoRepository>();
                services.AddTransient<IDispBBRegiaoFilialRepository, DispBBRegiaoFilialRepository>();
                services.AddTransient<IDispBBCriticidadeRepository, DispBBCriticidadeRepository>();
                services.AddTransient<IDispBBPercRegiaoRepository, DispBBPercRegiaoRepository>();
                services.AddTransient<IDispBBDesvioRepository, DispBBDesvioRepository>();
                services.AddTransient<IAgendaTecnicoRepository, AgendaTecnicoRepository>();
                services.AddTransient<IPontoUsuarioRepository, PontoUsuarioRepository>();

                // Servicos
                services.AddTransient<IFeriadoService, FeriadoService>();
                services.AddTransient<IDashboardService, DashboardService>();
                services.AddTransient<IIndicadorService, IndicadorService>();
                services.AddTransient<IAgendaTecnicoService, AgendaTecnicoService>();
                services.AddTransient<IPontoUsuarioService, PontoUsuarioService>();

                ServiceProvider serviceProvider = services.BuildServiceProvider();

                // Inicializa os servicos que serao usados
                this._indicadorService = serviceProvider.GetService<IIndicadorService>();
                this._agendaTecnicoService = serviceProvider.GetService<IAgendaTecnicoService>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}