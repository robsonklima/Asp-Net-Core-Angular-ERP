using System;
using System.Collections.Generic;

namespace SAT.MODELS.Entities
{
    public class RelatorioAtendimento
    {
        public int CodRAT { get; set; }
        public string NumRAT { get; set; }
        public string NomeRespCliente { get; set; }
        public string NomeAcompanhante { get; set; }
        public DateTime? DataHoraChegada { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime? DataHoraFim { get; set; }
        public DateTime DataHoraSolucao { get; set; }
        public string RelatoSolucao { get; set; }
        public string ObsRAT { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime? DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public int? CodServico { get; set; }
        public int? CodTipoEquip { get; set; }
        public int? CodGrupoEquip { get; set; }
        public int? CodEquip { get; set; }
        public DateTime? DataHoraAbertura { get; set; }
        public DateTime? DataHoraSolicitacao { get; set; }
        public DateTime? DataHoraReparo { get; set; }
        public short? QtdeHorasInicio { get; set; }
        public short? QtdeHorasReparo { get; set; }
        public short? QtdeHorasSolucao { get; set; }
        public short? QtdeHorasEspera { get; set; }
        public string MotivoEspera { get; set; }
        public short? QtdeHorasInterrupcao { get; set; }
        public string MotivoInterrupcao { get; set; }
        public short? QtdeHorasTecnicas { get; set; }
        public decimal? ValServicos { get; set; }
        public DateTime? DataCadastro { get; set; }
        public string CodUsuarioCadastro { get; set; }
        public DateTime? DataManutencao { get; set; }
        public string CodUsuarioManutencao { get; set; }
        public DateTime? TempoSlaInicio { get; set; }
        public DateTime? TempoSlaReparo { get; set; }
        public DateTime? TempoSlaSolucao { get; set; }
        public float? TempoEfetInicio { get; set; }
        public float? TempoEfetReparo { get; set; }
        public float? TempoEfetSolucao { get; set; }
        public string IndBRBAtendeConfederal { get; set; }
        public byte? IndRatDigitalizada { get; set; }
        public string CaminhoRATDigitalizada { get; set; }
        public int? IndQuarentena { get; set; }
        public DateTime? DataHoraInicioValida { get; set; }
        public DateTime? DataHoraSolucaoValida { get; set; }
        public DateTime? DataHoraFechamentoValida { get; set; }
        public byte? VentNotaBrb { get; set; }
        public byte? RepCasBRB { get; set; }
        public string CpuMecanismoDispensadorCedula { get; set; }
        public string CpuMecanismoDepositarioEnvelope { get; set; }
        public string CpuDispensadorEnvelope { get; set; }
        public string CpuImpressoraRecibo { get; set; }
        public string CpuPresenterFolhaCheque { get; set; }
        public string CpuAntiskimming { get; set; }
        public string PlacaSensor { get; set; }
        public string AceitadorCedula { get; set; }
        public string BiosCpuMicroComputador { get; set; }
        public string PertoScan { get; set; }
        public string TensaoSemCarga { get; set; }
        public string TensaoComCarga { get; set; }
        public string TemperaturaAmbiente { get; set; }
        public int? IndRedeEstabilizada { get; set; }
        public int? IndCedulaBoaQualidade { get; set; }
        public int? IndCedulaVentilada { get; set; }
        public int? IndInfraEstruturaLogicaAdequada { get; set; }
        public string TensaoTerraNeutro { get; set; }
        public TimeSpan? HorarioInicioIntervalo { get; set; }
        public TimeSpan? HorarioTerminoIntervalo { get; set; }
        public int? QtdPagamentos { get; set; }
        public int? QtdCedulasPagas { get; set; }
        public string NroSerieMecanismo { get; set; }
        public int CodOS { get; set; }
        public int CodTecnico { get; set; }
        public int? CodStatusServico { get; set; }
        public List<RelatorioAtendimentoDetalhe> RelatorioAtendimentoDetalhes { get; set; }
        public List<ProtocoloSTN> ProtocolosSTN { get; set; }
        public StatusServico StatusServico { get; set; }
        public Tecnico Tecnico { get; set; }
        public TipoServico TipoServico { get; set; }
        public List<Laudo> Laudos { get; set; }
        public List<CheckinCheckout> CheckinsCheckouts { get; set; }
        public List<Foto> Fotos { get; set; }
    }
}