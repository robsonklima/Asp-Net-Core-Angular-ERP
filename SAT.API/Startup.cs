using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SAT.API.Support;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.INFRA.Repository;
using SAT.SERVICES.Interfaces;
using SAT.SERVICES.Services;
using System;
using System.Text;

namespace SAT.API
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
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Prod")));

            services.AddCors(options =>
           {
               options.AddPolicy(name: "CorsApi",
                builder =>
                    {
                        builder.WithOrigins("https://sat-homologacao.perto.com.br", "http://localhost:4200")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    }
                );
           });

            services.AddMvc();
            services.AddSession();

            // Repositories
            services.AddTransient<IOrdemServicoRepository, OrdemServicoRepository>();
            services.AddTransient<IRelatorioAtendimentoRepository, RelatorioAtendimentoRepository>();
            services.AddTransient<IStatusServicoRepository, StatusServicoRepository>();
            services.AddTransient<ILocalAtendimentoRepository, LocalAtendimentoRepository>();
            services.AddTransient<IUnidadeFederativaRepository, UnidadeFederativaRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<ITipoIntervencaoRepository, TipoIntervencaoRepository>();
            services.AddTransient<IFilialRepository, FilialRepository>();
            services.AddTransient<IDefeitoRepository, DefeitoRepository>();
            services.AddTransient<IAcaoRepository, AcaoRepository>();
            services.AddTransient<ISequenciaRepository, SequenciaRepository>();
            services.AddTransient<IEquipamentoRepository, EquipamentoRepository>();
            services.AddTransient<IEquipamentoContratoRepository, EquipamentoContratoRepository>();
            services.AddTransient<IContratoRepository, ContratoRepository>();
            services.AddTransient<ITipoEquipamentoRepository, TipoEquipamentoRepository>();
            services.AddTransient<ITipoServicoRepository, TipoServicoRepository>();
            services.AddTransient<IRegiaoRepository, RegiaoRepository>();
            services.AddTransient<IAutorizadaRepository, AutorizadaRepository>();
            services.AddTransient<IPerfilRepository, PerfilRepository>();
            services.AddTransient<IPaisRepository, PaisRepository>();
            services.AddTransient<ICidadeRepository, CidadeRepository>();
            services.AddTransient<IPecaRepository, PecaRepository>();
            services.AddTransient<IGrupoCausaRepository, GrupoCausaRepository>();
            services.AddTransient<ITipoCausaRepository, TipoCausaRepository>();
            services.AddTransient<IGrupoEquipamentoRepository, GrupoEquipamentoRepository>();
            services.AddTransient<IAcordoNivelServicoRepository, AcordoNivelServicoRepository>();
            services.AddTransient<IRelatorioAtendimentoDetalheRepository, RelatorioAtendimentoDetalheRepository>();
            services.AddTransient<IRelatorioAtendimentoDetalhePecaRepository, RelatorioAtendimentoDetalhePecaRepository>();
            services.AddTransient<ITransportadoraRepository, TransportadoraRepository>();
            services.AddTransient<ITecnicoRepository, TecnicoRepository>();
            services.AddTransient<IFeriadoRepository, FeriadoRepository>();
            services.AddTransient<IRegiaoAutorizadaRepository, RegiaoAutorizadaRepository>();
            services.AddTransient<ICausaRepository, CausaRepository>();
            services.AddTransient<IDespesaCartaoCombustivelRepository, DespesaCartaoCombustivelRepository>();
            services.AddTransient<IAgendamentoRepository, AgendamentoRepository>();
            services.AddTransient<IMotivoAgendamentoRepository, MotivoAgendamentoRepository>();
            services.AddTransient<IContratoEquipamentoRepository, ContratoEquipamentoRepository>();
            services.AddTransient<IContratoSLARepository, ContratoSLARepository>();
            services.AddTransient<IAgendaTecnicoRepository, AgendaTecnicoRepository>();
            services.AddTransient<IGeolocalizacaoRepository, GeolocalizacaoRepository>();
            services.AddTransient<IOrdemServicoRelatorioInstalacaoRepository, OrdemServicoRelatorioInstalacaoRepository>();
            services.AddTransient<IOrdemServicoRelatorioInstalacaoNaoConformidadeRepository, OrdemServicoRelatorioInstalacaoNaoConformidadeRepository>();
            services.AddTransient<IOrdemServicoRelatorioInstalacaoItemRepository, OrdemServicoRelatorioInstalacaoItemRepository>();
            services.AddTransient<IInstalacaoRepository, InstalacaoRepository>();

            // Services
            services.AddTransient<IAcaoService, AcaoService>();
            services.AddTransient<IAcordoNivelServicoService, AcordoNivelServicoService>();
            services.AddTransient<IAutorizadaService, AutorizadaService>();
            services.AddTransient<ICausaService, CausaService>();
            services.AddTransient<ICidadeService, CidadeService>();
            services.AddTransient<IClienteService, ClienteService>();
            services.AddTransient<IContratoEquipamentoService, ContratoEquipamentoService>();
            services.AddTransient<IContratoService, ContratoService>();
            services.AddTransient<IContratoSLAService, ContratoSLAService>();
            services.AddTransient<IDefeitoService, DefeitoService>();
            services.AddTransient<IDespesaCartaoCombustivelService, DespesaCartaoCombustivelService>();
            services.AddTransient<IEquipamentoContratoService, EquipamentoContratoService>();
            services.AddTransient<IEquipamentoService, EquipamentoService>();
            services.AddTransient<IFeriadoService, FeriadoService>();
            services.AddTransient<IGrupoCausaService, GrupoCausaService>();
            services.AddTransient<IGrupoEquipamentoService, GrupoEquipamentoService>();
            services.AddTransient<ILocalAtendimentoService, LocalAtendimentoService>();
            services.AddTransient<IMotivoAgendamentoService, MotivoAgendamentoService>();
            services.AddTransient<IPaisService, PaisService>();
            services.AddTransient<IPecaService, PecaService>();
            services.AddTransient<IFilialService, FilialService>();
            services.AddTransient<IPerfilService, PerfilService>();
            services.AddTransient<IRegiaoAutorizadaService, RegiaoAutorizadaService>();
            services.AddTransient<IRegiaoService, RegiaoService>();
            services.AddTransient<IRelatorioAtendimentoDetalhePecaService, RelatorioAtendimentoDetalhePecaService>();
            services.AddTransient<IRelatorioAtendimentoDetalheService, RelatorioAtendimentoDetalheService>();
            services.AddTransient<IRelatorioAtendimentoService, RelatorioAtendimentoService>();
            services.AddTransient<IStatusServicoService, StatusServicoService>();
            services.AddTransient<ITecnicoService, TecnicoService>();
            services.AddTransient<ITipoCausaService, TipoCausaService>();
            services.AddTransient<ITipoEquipamentoService, TipoEquipamentoService>();
            services.AddTransient<ITipoIntervencaoService, TipoIntervencaoService>();
            services.AddTransient<ITipoServicoService, TipoServicoService>();
            services.AddTransient<ITransportadoraService, TransportadoraService>();
            services.AddTransient<IUnidadeFederativaService, UnidadeFederativaService>();
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IIndicadorService, IndicadorService>();
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IOrdemServicoService, OrdemServicoService>();
            services.AddTransient<IAgendaTecnicoService, AgendaTecnicoService>();
            services.AddTransient<IAgendamentoService, AgendamentoService>();
            services.AddTransient<IGeolocalizacaoService, GeolocalizacaoService>();
            services.AddTransient<IOrdemServicoRelatorioInstalacaoService, OrdemServicoRelatorioInstalacaoService>();
            services.AddTransient<IOrdemServicoRelatorioInstalacaoNaoConformidadeService, OrdemServicoRelatorioInstalacaoNaoConformidadeService>();
            services.AddTransient<IOrdemServicoRelatorioInstalacaoItemService, OrdemServicoRelatorioInstalacaoItemService>();
            services.AddTransient<IInstalacaoService,InstalacaoService>();

            services.AddSingleton<ILoggerService, LoggerService>();
            services.AddTransient<ITokenService, TokenService>();

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddControllers().AddNewtonsoftJson(options =>
              options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SAT_Api", Version = "v1" });
            });

            services.Configure<IISOptions>(o =>
            {
                o.ForwardClientCertificate = false;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SAT_Api v1"));
            }

            app.UseHttpsRedirection();
            app.UseCustomExceptionMiddleware();
            app.UseRouting();
            app.UseCors("CorsApi");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.Use(async (context, next) =>
            {
                var token = context.Session.GetString("Token");
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                }
                await next();
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
