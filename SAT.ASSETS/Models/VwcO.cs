using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcO
    {
        [Column("CodOS")]
        public int CodOs { get; set; }
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
        [Column("DescNumSerieNI")]
        [StringLength(20)]
        public string DescNumSerieNi { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraSolicitacao { get; set; }
        [Column("NumOSCliente")]
        [StringLength(20)]
        public string NumOscliente { get; set; }
        [Column("NumOSQuarteirizada")]
        [StringLength(20)]
        public string NumOsquarteirizada { get; set; }
        [StringLength(255)]
        public string NomeSolicitante { get; set; }
        [StringLength(20)]
        public string TelefoneSolicitante { get; set; }
        [StringLength(50)]
        public string NomeContato { get; set; }
        public string ObservacaoCliente { get; set; }
        [StringLength(1000)]
        public string DescMotivoMarcaEspecial { get; set; }
        public byte? IndMarcaEspecial { get; set; }
        [StringLength(20)]
        public string CodUsuarioMarcaEspecial { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataMarcaEspecial { get; set; }
        [StringLength(3500)]
        public string DefeitoRelatado { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraTransf { get; set; }
        [StringLength(20)]
        public string CodUsuarioFechamento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraFechamento { get; set; }
        [StringLength(1000)]
        public string MotivoCancelamento { get; set; }
        [StringLength(20)]
        public string CodUsuarioCancelamento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCancelamento { get; set; }
        public byte IndRevisaoReincidencia { get; set; }
        public byte? IndCienciaAtendente { get; set; }
        public byte? IndCienciaTerceirizada { get; set; }
        public byte? IndServico { get; set; }
        [StringLength(100)]
        public string ServicoEmail { get; set; }
        [Column("NumAgenciaNI")]
        [StringLength(10)]
        public string NumAgenciaNi { get; set; }
        [StringLength(5)]
        public string NumRemessa { get; set; }
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        public int? CodTipoMarcacao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TempoSlaInicio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TempoSlaReparo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TempoSlaSolucao { get; set; }
        [StringLength(100)]
        public string Endereco { get; set; }
        [StringLength(20)]
        public string Bairro { get; set; }
        [StringLength(30)]
        public string Cidade { get; set; }
        [Column("SiglaUF")]
        [StringLength(2)]
        public string SiglaUf { get; set; }
        [StringLength(20)]
        public string Pais { get; set; }
        [StringLength(20)]
        public string Cep { get; set; }
        public int? NumPatrimonio { get; set; }
        [Column("DataHoraAberturaOS", TypeName = "datetime")]
        public DateTime? DataHoraAberturaOs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraSolicAtendimento { get; set; }
        public byte? IndCancelado { get; set; }
        public byte? IndReincidencia { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataCadastro { get; set; }
        [StringLength(20)]
        public string CodUsuarioCadastro { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataManutencao { get; set; }
        [StringLength(20)]
        public string CodUsuarioManutencao { get; set; }
        [Column("IndAtrasoSLA")]
        public byte? IndAtrasoSla { get; set; }
        public float? TempoEfetInicio { get; set; }
        public float? TempoEfetReparo { get; set; }
        public float? TempoEfetSolucao { get; set; }
        public byte? IndAcertoParque { get; set; }
        public byte? IndEndossado { get; set; }
        [StringLength(20)]
        public string CodUsuarioAcertoParque { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAcertoParque { get; set; }
        [Column("IndRevOK")]
        public byte? IndRevOk { get; set; }
        public int? CodTipoEquip { get; set; }
        public int? CodGrupoEquip { get; set; }
        public byte? IndOrcamentoEnd { get; set; }
        public byte? IndEnderecoRevisado { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraEnderecoVerificado { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraIntegracaoRevisao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraIntegracaoRevisaoAgendamento { get; set; }
        public byte? IndAgendamentoReenviado { get; set; }
        [Column("DataHoraOSMobileRecebida", TypeName = "datetime")]
        public DateTime? DataHoraOsmobileRecebida { get; set; }
        [Column("DataHoraOSMobileLida", TypeName = "datetime")]
        public DateTime? DataHoraOsmobileLida { get; set; }
        [StringLength(30)]
        public string NumAgenciaBanco { get; set; }
        [StringLength(40)]
        public string NumContaEstabelecimentoCliente { get; set; }
        [Column("CNPJEstabelecimentoCliente")]
        [StringLength(30)]
        public string CnpjestabelecimentoCliente { get; set; }
        [StringLength(50)]
        public string RazaoSocialEstabelecimentoCliente { get; set; }
        [StringLength(50)]
        public string RedeEquipamento { get; set; }
        [StringLength(35)]
        public string NumTerminal { get; set; }
        public byte? IndServicoVerificado { get; set; }
        public int? IndIntegracao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraEnvioAgendamentoSemat { get; set; }
        public byte? IndAgendamentoUnico { get; set; }
        public byte? IndNotificacaoOrcamentoEnviado { get; set; }
        public int? CodMotivoCancelamentoBanrisul { get; set; }
        [StringLength(30)]
        public string NomeArquivoIntegracaoBanrisul { get; set; }
        public byte? IndReaberturaIntegracaoBanrisul { get; set; }
        [Column("IndOSIntervencaoEquipamento")]
        public byte? IndOsintervencaoEquipamento { get; set; }
        public int? IndBloqueioReincidencia { get; set; }
        [StringLength(50)]
        public string NomeEquip { get; set; }
        [StringLength(20)]
        public string NumSerie { get; set; }
        [StringLength(50)]
        public string NomTipoIntervencao { get; set; }
        [Column("CodETipoIntervencao")]
        [StringLength(5)]
        public string CodEtipoIntervencao { get; set; }
        [Column("DataFimSLA", TypeName = "datetime")]
        public DateTime? DataFimSla { get; set; }
        [StringLength(66)]
        public string ClienteLocal { get; set; }
        [StringLength(50)]
        public string NomeFantasia { get; set; }
        [Column("MARCACAOESPECIAL")]
        [StringLength(1000)]
        public string Marcacaoespecial { get; set; }
        [Column("DataAberturaOS")]
        [StringLength(30)]
        public string DataAberturaOs { get; set; }
        [Column("HoraAberturaOS")]
        [StringLength(30)]
        public string HoraAberturaOs { get; set; }
        [StringLength(30)]
        public string DataSolicitacao { get; set; }
        [StringLength(30)]
        public string HoraSolicitacao { get; set; }
        [Column("DtFimSLA")]
        [StringLength(30)]
        public string DtFimSla { get; set; }
        [Column("HrFimSLA")]
        [StringLength(30)]
        public string HrFimSla { get; set; }
        [StringLength(31)]
        public string NumReincidencia { get; set; }
        [StringLength(20)]
        public string CodMagnus { get; set; }
        public int? QtdPagamentos { get; set; }
        public int? QtdCedulasPagas { get; set; }
        [StringLength(50)]
        public string NroSerieMecanismo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraAgendamento { get; set; }
    }
}
