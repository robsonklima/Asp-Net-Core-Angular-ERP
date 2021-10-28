using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Mapping;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<OrdemServico> OrdemServico { get; set; }
        public DbSet<RelatorioAtendimento> RelatorioAtendimento { get; set; }
        public DbSet<RelatorioAtendimentoDetalhe> RelatorioAtendimentoDetalhe { get; set; }
        public DbSet<RelatorioAtendimentoDetalhePeca> RelatorioAtendimentoDetalhePeca { get; set; }
        public DbSet<StatusServico> StatusServico { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Sequencia> Sequencia { get; set; }
        public DbSet<Tecnico> Tecnico { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<MotivoAgendamento> MotivoAgendamento { get; set; }
        public DbSet<TipoIntervencao> TipoIntervencao { get; set; }
        public DbSet<TipoServico> TipoServico { get; set; }
        public DbSet<LocalAtendimento> LocalAtendimento { get; set; }
        public DbSet<Equipamento> Equipamento { get; set; }
        public DbSet<GrupoEquipamento> GrupoEquipamento { get; set; }
        public DbSet<TipoEquipamento> TipoEquipamento { get; set; }
        public DbSet<EquipamentoContrato> EquipamentoContrato { get; set; }
        public DbSet<Contrato> Contrato { get; set; }
        public DbSet<Filial> Filial { get; set; }
        public DbSet<Defeito> Defeito { get; set; }
        public DbSet<Acao> Acao { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<Regiao> Regiao { get; set; }
        public DbSet<Agendamento> Agendamento { get; set; }
        public DbSet<DespesaCartaoCombustivel> DespesaCartaoCombustivel { get; set; }
        public DbSet<Autorizada> Autorizada { get; set; }
        public DbSet<AcordoNivelServico> AcordoNivelServico { get; set; }
        public DbSet<Navegacao> Navegacao { get; set; }
        public DbSet<NavegacaoConfiguracao> NavegacaoConfiguracao { get; set; }
        public DbSet<Pais> Pais { get; set; }
        public DbSet<UnidadeFederativa> UnidadeFederativa { get; set; }
        public DbSet<Cidade> Cidade { get; set; }
        public DbSet<Peca> Peca { get; set; }
        public DbSet<Transportadora> Transportadora { get; set; }
        public DbSet<Feriado> Feriado { get; set; }
        public DbSet<Causa> Causa { get; set; }
        public DbSet<GrupoCausa> GrupoCausa { get; set; }
        public DbSet<TipoCausa> TipoCausa { get; set; }
        public DbSet<RegiaoAutorizada> RegiaoAutorizada { get; set; }
        public DbSet<Localizacao> Localizacao { get; set; }
        public DbSet<ContratoEquipamento> ContratoEquipamento { get; set; }
        public DbSet<ContratoSLA> ContratoSLA { get; set; }
        public DbSet<AgendaTecnico> AgendaTecnico { get; set; }
        public DbSet<Geolocalizacao> Geolocalizacao { get; set; }
        public DbSet<Monitoramento> Monitoramento { get; set; }
        public DbSet<DispBBCriticidade> DispBBCriticidade { get; set; }
        public DbSet<DispBBRegiaoFilial> DispBBRegiaoFilial { get; set; }
        public DbSet<DispBBPercRegiao> DispBBPercRegiao { get; set; }
        public DbSet<DispBBDesvio> DispBBDesvio { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Sequencia>(new SequenciaMap().Configure);
            modelBuilder.Entity<ContratoEquipamento>(new ContratoEquipamentoMap().Configure);
            modelBuilder.Entity<ContratoSLA>(new ContratoSLAMap().Configure);

            modelBuilder.Entity<RegiaoAutorizada>()
                        .HasKey(ra => new { ra.CodFilial, ra.CodRegiao, ra.CodAutorizada });

            modelBuilder.Entity<NavegacaoConfiguracao>()
                        .HasOne<Perfil>(nc => nc.Perfil)
                        .WithMany(nc => nc.NavegacoesConfiguracao);

            modelBuilder.Entity<NavegacaoConfiguracao>()
                        .HasOne<Navegacao>(nc => nc.Navegacao)
                        .WithMany(nc => nc.NavegacoesConfiguracao);

            modelBuilder.Entity<Tecnico>()
                        .HasMany<OrdemServico>(os => os.OrdensServico);
        }
    }
}
