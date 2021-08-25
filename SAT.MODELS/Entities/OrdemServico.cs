using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    [Table("OS")]
    public class OrdemServico
    {
        [Key]
        public int CodOS { get; set; }
        [ForeignKey("CodStatusServico")]
        public StatusServico StatusServico { get; set; }
        [ForeignKey("CodTipoIntervencao")]
        public TipoIntervencao TipoIntervencao { get; set; }
        [ForeignKey("CodPosto")]
        public LocalAtendimento LocalAtendimento { get; set; }
        [ForeignKey("CodEquipContrato")]
        public EquipamentoContrato EquipamentoContrato { get; set; }
        [ForeignKey("CodEquip")]
        public Equipamento Equipamento { get; set; }
        [ForeignKey("CodOS")]
        public List<RelatorioAtendimento> RelatoriosAtendimento { get; set; }
        [ForeignKey("CodOS")]
        public List<Foto> Fotos { get; set; }
        [ForeignKey("CodCliente")]
        public Cliente Cliente { get; set; }
        [ForeignKey("CodTecnico")]
        public Tecnico Tecnico { get; set; }
        [ForeignKey("CodOS")]
        public StatusSLAOSFechada StatusSLAOSFechada { get; set; }
        [ForeignKey("CodOS")]
        public StatusSLAOSAberta StatusSLAOSAberta { get; set; }
        [ForeignKey("CodOS")]
        public List<Agendamento> Agendamentos { get; set; }
        public string DefeitoRelatado { get; set; }
        public string ObservacaoCliente { get; set; }
        public DateTime? DataHoraCad { get; set; }
        public DateTime? DataHoraTransf { get; set; }
        public DateTime? DataHoraFechamento { get; set; }
        public string CodUsuarioCad { get; set; }
        public string NomeSolicitante { get; set; }
        public string TelefoneSolicitante { get; set; }
        public int CodCliente { get; set; }
        public int CodPosto { get; set; }
        public int? CodEquipContrato { get; set; }
        public int? CodEquip { get; set; }
        public int CodTipoIntervencao { get; set; }
        public int? CodFilial { get; set; }
        public int? CodAutorizada { get; set; }
        public int? CodRegiao { get; set; }
        public int CodStatusServico { get; set; }
        public int? CodTecnico { get; set; }
        public string DescNumSerieNi { get; set; }
        public DateTime? DataHoraSolicitacao { get; set; }
        public string NumOSCliente { get; set; }
        public string NumOSQuarteirizada { get; set; }
        public string NomeContato { get; set; }
        public string DescMotivoMarcaEspecial { get; set; }
        public byte? IndMarcaEspecial { get; set; }
        public string CodUsuarioMarcaEspecial { get; set; }
        public DateTime? DataMarcaEspecial { get; set; }
        public string CodUsuarioFechamento { get; set; }
        public string MotivoCancelamento { get; set; }
        public string CodUsuarioCancelamento { get; set; }
        public DateTime? DataHoraCancelamento { get; set; }
        public byte IndRevisaoReincidencia { get; set; }
        public byte? IndCienciaAtendente { get; set; }
        public byte? IndCienciaTerceirizada { get; set; }
        public byte? IndServico { get; set; }
        public string ServicoEmail { get; set; }
        public string NumAgenciaNI { get; set; }
        public string NumRemessa { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public int? CodTipoMarcacao { get; set; }
        public DateTime? TempoSlaInicio { get; set; }
        public DateTime? TempoSlaReparo { get; set; }
        public DateTime? TempoSlaSolucao { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string SiglaUF { get; set; }
        public string Pais { get; set; }
        public string Cep { get; set; }
        public int? NumPatrimonio { get; set; }
        public DateTime? DataHoraAberturaOS { get; set; }
        public DateTime? DataHoraSolicAtendimento { get; set; }
        public byte? IndCancelado { get; set; }
        public byte? IndReincidencia { get; set; }
        public DateTime? DataCadastro { get; set; }
        public string CodUsuarioCadastro { get; set; }
        public DateTime? DataManutencao { get; set; }
        public string CodUsuarioManutencao { get; set; }
        public byte? IndAtrasoSLA { get; set; }
        public float? TempoEfetInicio { get; set; }
        public float? TempoEfetReparo { get; set; }
        public float? TempoEfetSolucao { get; set; }
        public byte? IndAcertoParque { get; set; }
        public byte? IndEndossado { get; set; }
        public string CodUsuarioAcertoParque { get; set; }
        public DateTime? DataAcertoParque { get; set; }
        public byte? IndRevOk { get; set; }
        public int? CodTipoEquip { get; set; }
        public int? CodGrupoEquip { get; set; }
        public byte? IndOrcamentoEnd { get; set; }
        public byte? IndEnderecoRevisado { get; set; }
        public DateTime? DataHoraEnderecoVerificado { get; set; }
        public DateTime? DataHoraIntegracaoRevisao { get; set; }
        public DateTime? DataHoraIntegracaoRevisaoAgendamento { get; set; }
        public byte? IndAgendamentoReenviado { get; set; }
        public DateTime? DataHoraOsmobileRecebida { get; set; }
        public DateTime? DataHoraOsmobileLida { get; set; }
        public string NumAgenciaBanco { get; set; }
        public string NumContaEstabelecimentoCliente { get; set; }
        public string CnpjestabelecimentoCliente { get; set; }
        public string RazaoSocialEstabelecimentoCliente { get; set; }
        public string RedeEquipamento { get; set; }
        public string NumTerminal { get; set; }
        public byte? IndServicoVerificado { get; set; }
        public int? IndIntegracao { get; set; }
        public DateTime? DataHoraEnvioAgendamentoSemat { get; set; }
        public byte? IndAgendamentoUnico { get; set; }
        public byte? IndNotificacaoOrcamentoEnviado { get; set; }
        public int? CodMotivoCancelamentoBanrisul { get; set; }
        public string NomeArquivoIntegracaoBanrisul { get; set; }
        public byte? IndReaberturaIntegracaoBanrisul { get; set; }
        public byte? IndOSIntervencaoEquipamento { get; set; }
        public byte? IndLiberacaoFechaduraCofre { get; set; }
        public byte? IndExclusaoBanrisul { get; set; }
        public DateTime? DataExclusaoBanrisul { get; set; }
        public int? IndBloqueioReincidencia { get; set; }
        public int? NumReincidencia { get; set; }
        public byte? IndVandalismo { get; set; }
        public int? IndStatusEnvioReincidencia { get; set; }
        public byte? IndFechamentoBanrisul { get; set; }
        public DateTime? DataFechamentoBanrisul { get; set; }
        public string ObsFechamentoBanrisul { get; set; }
        public string NumEstabelecimentoCliente { get; set; }
        public int? CodSeveridade { get; set; }
        public int? CodContrato { get; set; }
        public string SugestaoOS { get; set; }
        public int? CodMotivoCancelamento { get; set; }
        public int? CodOperadoraTelefonia { get; set; }
        public int? CodDefeitoPOS { get; set; }
        public string CodUsuarioOSMobileLida { get; set; }
        public string CodUsuarioOsmobileRecebida { get; set; }
        public DateTime? DataHoraIntegracaoRevisaoV2 { get; set; }
        public DateTime? DataHoraIntegracaoRevisaoAgendamentoV2 { get; set; }
        [NotMapped]
        public List<Alerta> Alertas { get; set; }
        [ForeignKey("CodFilial, CodRegiao, CodAutorizada")]
        public RegiaoAutorizada RegiaoAutorizada { get; set; }
    }
}
