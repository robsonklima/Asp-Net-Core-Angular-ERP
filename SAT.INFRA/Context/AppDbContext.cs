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
        public DbSet<PontoMovel> PontoMovel { get; set; }
        public DbSet<PontoMovelTipoHorario> PontoMovelTipoHorario { get; set; }
        public DbSet<PontoPeriodo> PontoPeriodo { get; set; }
        public DbSet<PontoPeriodoIntervaloAcessoData> PontoPeriodoIntervaloAcessoData { get; set; }
        public DbSet<PontoPeriodoModoAprovacao> PontoPeriodoModoAprovacao { get; set; }
        public DbSet<PontoPeriodoStatus> PontoPeriodoStatus { get; set; }
        public DbSet<PontoPeriodoUsuario> PontoPeriodoUsuario { get; set; }
        public DbSet<PontoPeriodoUsuarioStatus> PontoPeriodoUsuarioStatus { get; set; }
        public DbSet<PontoSobreAviso> PontoSobreAviso { get; set; }
        public DbSet<PontoTipoHora> PontoTipoHora { get; set; }
        public DbSet<PontoUsuario> PontoUsuario { get; set; }
        public DbSet<PontoUsuarioData> PontoUsuarioData { get; set; }
        public DbSet<PontoUsuarioDataAdvertencia> PontoUsuarioDataAdvertencia { get; set; }
        public DbSet<PontoUsuarioDataControleAlteracaoAcesso> PontoUsuarioDataControleAlteracaoAcesso { get; set; }
        public DbSet<PontoUsuarioDataDivergencia> PontoUsuarioDataDivergencia { get; set; }
        public DbSet<PontoUsuarioDataDivergenciaObservacao> PontoUsuarioDataDivergenciaObservacao { get; set; }
        public DbSet<PontoUsuarioDataDivergenciaRAT> PontoUsuarioDataDivergenciaRAT { get; set; }
        public DbSet<PontoUsuarioDataJustificativaAlteracaoAcesso> PontoUsuarioDataJustificativaAlteracaoAcesso { get; set; }
        public DbSet<PontoUsuarioDataJustificativaValidacao> PontoUsuarioDataJustificativaValidacao { get; set; }
        public DbSet<PontoUsuarioDataModoAlteracaoAcesso> PontoUsuarioDataModoAlteracaoAcesso { get; set; }
        public DbSet<PontoUsuarioDataModoDivergencia> PontoUsuarioDataModoDivergencia { get; set; }
        public DbSet<PontoUsuarioDataMotivoDivergencia> PontoUsuarioDataMotivoDivergencia { get; set; }
        public DbSet<PontoUsuarioDataStatus> PontoUsuarioDataStatus { get; set; }
        public DbSet<PontoUsuarioDataStatusAcesso> PontoUsuarioDataStatusAcesso { get; set; }
        public DbSet<PontoUsuarioDataTipoAdvertencia> PontoUsuarioDataTipoAdvertencia { get; set; }
        public DbSet<PontoUsuarioDataValidacao> PontoUsuarioDataValidacao { get; set; }
        public DbSet<PontoUsuarioRejeicao> PontoUsuarioRejeicao { get; set; }
        public DbSet<PlantaoTecnico> PlantaoTecnico { get; set; }
        public DbSet<DespesaAdiantamentoPeriodo> DespesaAdiantamentoPeriodo { get; set; }
        public DbSet<DespesaAdiantamento> DespesaAdiantamento { get; set; }
        public DbSet<DespesaConfiguracaoCombustivel> DespesaConfiguracaoCombustivel { get; set; }
        public DbSet<DespesaItem> DespesaItem { get; set; }
        public DbSet<DespesaPeriodo> DespesaPeriodo { get; set; }
        public DbSet<DespesaPeriodoTecnico> DespesaPeriodoTecnico { get; set; }
        public DbSet<Despesa> Despesa { get; set; }
        public DbSet<DespesaTipo> DespesaTipo { get; set; }
        public DbSet<DespesaCartaoCombustivelTecnico> DespesaCartaoCombustivelTecnico { get; set; }

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

            modelBuilder.Entity<DespesaPeriodoTecnico>()
                        .HasKey(ra => new { ra.CodTecnico, ra.CodDespesaPeriodo });
        }
    }
}