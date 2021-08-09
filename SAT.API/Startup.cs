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
using SAT.API.Context;
using SAT.API.Repositories;
using SAT.API.Repositories.Interfaces;
using SAT.API.Services;
using SAT.API.Services.Interfaces;
using SAT.API.Support;
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
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Homolog")));

            services.AddCors(options =>
            {
                options.AddPolicy("CorsApi",
                    builder => builder
                        .WithOrigins("https://sat-homologacao.perto.com.br")
                        .WithOrigins("https://sat.perto.com.br")
                        .WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithExposedHeaders("X-Pagination"));
            });
            services.AddMvc();
            services.AddSession();

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
            services.AddTransient<INavegacaoRepository, NavegacaoRepository>();
            services.AddTransient<IAutorizadaRepository, AutorizadaRepository>();
            services.AddTransient<IPerfilRepository, PerfilRepository>();
            services.AddTransient<IPaisRepository, PaisRepository>();
            services.AddTransient<ICidadeRepository, CidadeRepository>();
            services.AddTransient<IPecaRepository, PecaRepository>();
            services.AddTransient<IGrupoCausaRepository, GrupoCausaRepository>();
            services.AddTransient<ITipoCausaRepository, TipoCausaRepository>();
            services.AddTransient<IGrupoEquipamentoRepository, GrupoEquipamentoRepository>();
            services.AddTransient<IAcordoNivelServicoRepository, AcordoNivelServicoRepository>();
            services.AddTransient<ITraducaoRepository, TraducaoRepository>();
            services.AddTransient<ITransportadoraRepository, TransportadoraRepository>();
            services.AddTransient<ITecnicoRepository, TecnicoRepository>();
            services.AddTransient<IFeriadoRepository, FeriadoRepository>();
            services.AddTransient<IRegiaoAutorizadaRepository, RegiaoAutorizadaRepository>();
            services.AddTransient<ICausaRepository, CausaRepository>();
            services.AddTransient<ITecnicoOrdemServicoRepository, TecnicoOrdemServicoRepository>();
            services.AddTransient<IAgendamentoRepository, AgendamentoRepository>();
            services.AddTransient<IMotivoAgendamentoRepository, MotivoAgendamentoRepository>();
            services.AddSingleton<ILoggerRepository, LoggerRepository>();
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
