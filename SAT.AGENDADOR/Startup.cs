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
        private IAgendaTecnicoService _agendaTecnicoService;

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
                services.AddDbContext<AppDbContext>(options => options.UseSqlServer(ConfigurationManager.AppSettings["Homolog"]));
                //#if DEBUG
                //                services.AddDbContext<AppDbContext>(options => options.UseSqlServer(ConfigurationManager.AppSettings["Homolog"]));
                //#else
                //                services.AddDbContext<AppDbContext>(options => options.UseSqlServer(ConfigurationManager.AppSettings["Prod"]));
                //#endif

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
                services.AddTransient<IMediaAtendimentoTecnicoRepository, MediaAtendimentoTecnicoRepository>();
                services.AddTransient<IRelatorioAtendimentoRepository, RelatorioAtendimentoRepository>();


                services.AddTransient<IFeriadoService, FeriadoService>();
                services.AddTransient<IDashboardService, DashboardService>();
                services.AddTransient<IAgendaTecnicoService, AgendaTecnicoService>();
                services.AddTransient<IPontoUsuarioService, PontoUsuarioService>();
                services.AddTransient<IEmailService, EmailService>();

                ServiceProvider serviceProvider = services.BuildServiceProvider();

                // Inicializa os servicos que serao usados
                this._agendaTecnicoService = serviceProvider.GetService<IAgendaTecnicoService>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}