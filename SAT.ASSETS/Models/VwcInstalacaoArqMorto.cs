using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcInstalacaoArqMorto
    {
        public int CodContrato { get; set; }
        public int CodInstalacao { get; set; }
        [StringLength(50)]
        public string NomeFilial { get; set; }
        [StringLength(50)]
        public string NomeAutorizada { get; set; }
        [StringLength(50)]
        public string NomeRegiao { get; set; }
        [Column("NomeSLA")]
        [StringLength(50)]
        public string NomeSla { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeLote { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataRecLote { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeEquip { get; set; }
        [StringLength(20)]
        public string NumSerie { get; set; }
        [StringLength(20)]
        public string NumSerieCliente { get; set; }
        [Required]
        [StringLength(11)]
        public string Agencia { get; set; }
        [StringLength(50)]
        public string NomeLocal { get; set; }
        [Column("CNPJ")]
        [StringLength(20)]
        public string Cnpj { get; set; }
        [Required]
        [StringLength(253)]
        public string Endereco { get; set; }
        [StringLength(50)]
        public string NomeCidade { get; set; }
        [Column("SiglaUF")]
        [StringLength(50)]
        public string SiglaUf { get; set; }
        [Column("CEP")]
        [StringLength(8)]
        public string Cep { get; set; }
        [Column("DataRecDM", TypeName = "datetime")]
        public DateTime? DataRecDm { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataLimiteEnt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataSugEntrega { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataConfEntrega { get; set; }
        [Column("NFRemessa")]
        [StringLength(20)]
        public string Nfremessa { get; set; }
        [Column("DataNFRemessa", TypeName = "datetime")]
        public DateTime? DataNfremessa { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataExpedicao { get; set; }
        [StringLength(50)]
        public string NomeTransportadora { get; set; }
        [Required]
        [StringLength(11)]
        public string AgenciaEnt { get; set; }
        [StringLength(50)]
        public string NomeLocalEnt { get; set; }
        [Column("DataHoraChegTranspBT", TypeName = "datetime")]
        public DateTime? DataHoraChegTranspBt { get; set; }
        [Column("IndEquipPosicOKBT")]
        public byte? IndEquipPosicOkbt { get; set; }
        [Column("NomeRespBancoBT")]
        [StringLength(50)]
        public string NomeRespBancoBt { get; set; }
        [Column("NumMatriculaBT")]
        [StringLength(20)]
        public string NumMatriculaBt { get; set; }
        [Column("IndBTOrigEnt")]
        public byte? IndBtorigEnt { get; set; }
        [Column("IndBTOK")]
        public byte? IndBtok { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataLimiteIns { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataSugInstalacao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataConfInstalacao { get; set; }
        [Column("CodOS")]
        public int? CodOs { get; set; }
        [Column("DataHoraOS", TypeName = "datetime")]
        public DateTime? DataHoraOs { get; set; }
        [StringLength(50)]
        public string NomeStatusServico { get; set; }
        [Column("NumRAT")]
        [StringLength(20)]
        public string NumRat { get; set; }
        [Required]
        [StringLength(11)]
        public string AgenciaIns { get; set; }
        [StringLength(50)]
        public string NomeLocalIns { get; set; }
        [Column("DataBI", TypeName = "datetime")]
        public DateTime? DataBi { get; set; }
        [Column("QtdParaboldBI")]
        public int? QtdParaboldBi { get; set; }
        [Column("IndEquipRebaixadoBI")]
        public byte? IndEquipRebaixadoBi { get; set; }
        [Column("NomeRespBancoBI")]
        [StringLength(50)]
        public string NomeRespBancoBi { get; set; }
        [Column("NumMatriculaBI")]
        [StringLength(20)]
        public string NumMatriculaBi { get; set; }
        [Column("IndBIOrigEnt")]
        public byte? IndBiorigEnt { get; set; }
        [Column("IndBIOK")]
        public byte? IndBiok { get; set; }
        [Column("IndLaudoOK")]
        public byte? IndLaudoOk { get; set; }
        [Column("IndRE5330OK")]
        public byte? IndRe5330ok { get; set; }
        [Column("IndRATOK")]
        public byte? IndRatok { get; set; }
        [Column("NFAut")]
        [StringLength(20)]
        public string Nfaut { get; set; }
        [Column("DataNFAut", TypeName = "datetime")]
        public DateTime? DataNfaut { get; set; }
        [Column("NumNFVenda")]
        public int? NumNfvenda { get; set; }
        [Column("DataNFVenda", TypeName = "datetime")]
        public DateTime? DataNfvenda { get; set; }
        [Column("DataNFVendaEnvioCliente", TypeName = "datetime")]
        public DateTime? DataNfvendaEnvioCliente { get; set; }
        [Column("DataNFVendaRecebimentoCliente", TypeName = "datetime")]
        public DateTime? DataNfvendaRecebimentoCliente { get; set; }
        public byte? TermoDescaracterizacao { get; set; }
        [StringLength(80)]
        public string FornecedorTradeIn1 { get; set; }
        [StringLength(80)]
        public string FornecedorTradeIn2 { get; set; }
        [StringLength(100)]
        public string BemTradeIn { get; set; }
        [StringLength(100)]
        public string Fabricante { get; set; }
        [StringLength(50)]
        public string Modelo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataRetirada { get; set; }
        [Column("NFTradeIn1")]
        [StringLength(20)]
        public string NftradeIn1 { get; set; }
        [Column("NFTradeIn2")]
        [StringLength(20)]
        public string NftradeIn2 { get; set; }
        [Column("DataNFTradeIn1", TypeName = "datetime")]
        public DateTime? DataNftradeIn1 { get; set; }
        [Column("DataNFTradeIn2", TypeName = "datetime")]
        public DateTime? DataNftradeIn2 { get; set; }
        [StringLength(50)]
        public string TransportadoraAcompanhamento { get; set; }
        [StringLength(50)]
        public string NomeReponsavelBancoAcompanhamento { get; set; }
        [StringLength(20)]
        public string NumMatriculaRespBancoAcompanhamento { get; set; }
        [StringLength(10)]
        public string NumFaturaTransp { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataPagtoEntrega { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? VlrPagtoEntrega { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataPagtoInstalacao { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? VlrPagtoInstalacao { get; set; }
        [StringLength(50)]
        public string NroContrato { get; set; }
        [StringLength(50)]
        public string PedidoCompra { get; set; }
        [StringLength(25)]
        public string SuperE { get; set; }
        [Column("CSL")]
        [StringLength(25)]
        public string Csl { get; set; }
        [Column("CSOServ")]
        [StringLength(25)]
        public string Csoserv { get; set; }
        [StringLength(25)]
        public string Supridora { get; set; }
        [Column("MST606TipoNovo")]
        [StringLength(25)]
        public string Mst606tipoNovo { get; set; }
        [Column("vlrDesFixacao1", TypeName = "decimal(10, 2)")]
        public decimal VlrDesFixacao1 { get; set; }
        [Column("vlrKMTradeIn1", TypeName = "decimal(10, 2)")]
        public decimal VlrKmtradeIn1 { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal DistanciaKmTradeIn1 { get; set; }
        [Column("vlrDesFixacao2", TypeName = "decimal(10, 2)")]
        public decimal VlrDesFixacao2 { get; set; }
        [Column("vlrKMTradeIn2", TypeName = "decimal(10, 2)")]
        public decimal VlrKmtradeIn2 { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal DistanciaKmTradeIn2 { get; set; }
        [Column(TypeName = "numeric(10, 2)")]
        public decimal VlrTotalTradeIn1 { get; set; }
        [Column(TypeName = "numeric(10, 2)")]
        public decimal VlrTotalTradeIn2 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DtPrevRecolhimentoTradeIn { get; set; }
        [StringLength(30)]
        public string Romaneio { get; set; }
        [Column("NFRemessaConferida")]
        [StringLength(50)]
        public string NfremessaConferida { get; set; }
        [Column("NFTransportadoraTradeIn")]
        [StringLength(20)]
        public string NftransportadoraTradeIn { get; set; }
        [Column("DataNFTransportadoraTradeIn", TypeName = "datetime")]
        public DateTime? DataNftransportadoraTradeIn { get; set; }
        [Column("vlrRecolhimentoTradeIn", TypeName = "decimal(10, 2)")]
        public decimal? VlrRecolhimentoTradeIn { get; set; }
        [StringLength(80)]
        public string FornecedorCompraTradeIn { get; set; }
        [Column("NFVendaTradeIn")]
        [StringLength(20)]
        public string NfvendaTradeIn { get; set; }
        [Column("DataNFVendaTradeIn", TypeName = "datetime")]
        public DateTime? DataNfvendaTradeIn { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValorUnitarioVenda { get; set; }
        [StringLength(20)]
        public string DtbCliente { get; set; }
        [StringLength(20)]
        public string FaturaTranspReEntrega { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DtReEntrega { get; set; }
        [StringLength(50)]
        public string ResponsavelRecebReEntrega { get; set; }
        public byte? IndInstalacao { get; set; }
        public int? CodInstalLote { get; set; }
        public int? CodFilial { get; set; }
        public int? CodAutorizada { get; set; }
        public int? CodRegiao { get; set; }
        public int CodTipoEquip { get; set; }
        public int CodGrupoEquip { get; set; }
        public int? CodEquip { get; set; }
        [StringLength(6)]
        public string NumAgencia { get; set; }
        [Column("DCPosto")]
        [StringLength(4)]
        public string Dcposto { get; set; }
        public int CodContratoEquipDataEnt { get; set; }
        public int Expr1 { get; set; }
        public int QtdLimDiaEnt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAssinatura { get; set; }
        public int Expr2 { get; set; }
        public int Expr3 { get; set; }
        public int Expr4 { get; set; }
        public int Expr5 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Expr6 { get; set; }
        public int Expr7 { get; set; }
        public int? CodTransportadora { get; set; }
        public int CodInstalStatus { get; set; }
        [Column("Filial_Autorizada_Regiao")]
        [StringLength(92)]
        public string FilialAutorizadaRegiao { get; set; }
        [Column("TipoEquip_GrupoEquip_CodEquip")]
        [StringLength(92)]
        public string TipoEquipGrupoEquipCodEquip { get; set; }
        [Column("CodUF")]
        public int? CodUf { get; set; }
        [StringLength(20)]
        public string CodUsuarioBlock { get; set; }
        public int QtdDiaAtrasoEnt { get; set; }
        public int QtdDiaAtrasoIns { get; set; }
        public int? CodEquipContrato { get; set; }
        public int QtdDiaGarantia { get; set; }
        public int? CodContratoEquipDataGar { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataGar { get; set; }
        public byte? IndGarPriSem { get; set; }
        public byte? IndGarSegSem { get; set; }
        public byte? IndGarTerSem { get; set; }
        public byte? IndGarQuaSem { get; set; }
        public byte? IndGarPriQui { get; set; }
        public byte? IndGarSegQui { get; set; }
        [Required]
        [StringLength(1)]
        public string IndObs { get; set; }
        [Required]
        [StringLength(1)]
        public string IndAnexo { get; set; }
        public int? CodFilialIns { get; set; }
        [StringLength(92)]
        public string CodFilAutReg { get; set; }
        [StringLength(92)]
        public string CodEquipamento { get; set; }
        public int CodCliente { get; set; }
        public int? CodPosto { get; set; }
        public int? CodClienteEnt { get; set; }
        public int? CodPostoEnt { get; set; }
        public int? CodStatusServico { get; set; }
        public int? CodPostoIns { get; set; }
        [Column("CodRAT")]
        public int? CodRat { get; set; }
        public int? CodClienteIns { get; set; }
        [Required]
        [StringLength(3)]
        public string IndRessalvaInsR { get; set; }
        [Required]
        [StringLength(3)]
        public string IndRessalvaInsF { get; set; }
        [Required]
        [StringLength(3)]
        public string IndRessalvaEnt { get; set; }
        public byte? InstalacaoSimNao { get; set; }
        [Column("CodInstalNFVenda")]
        public int? CodInstalNfvenda { get; set; }
        [StringLength(50)]
        public string NomeUsuarioBlock { get; set; }
        [Column("DTVencBord100", TypeName = "datetime")]
        public DateTime? DtvencBord100 { get; set; }
        [Column("DTEntBord100", TypeName = "datetime")]
        public DateTime? DtentBord100 { get; set; }
        [Column("DTVencBord90", TypeName = "datetime")]
        public DateTime? DtvencBord90 { get; set; }
        [Column("DTEntBord90", TypeName = "datetime")]
        public DateTime? DtentBord90 { get; set; }
        [Column("DTVencBord10", TypeName = "datetime")]
        public DateTime? DtvencBord10 { get; set; }
        [Column("DTEntBord10", TypeName = "datetime")]
        public DateTime? DtentBord10 { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValorFrete1 { get; set; }
        [StringLength(10)]
        public string FaturaFrete1 { get; set; }
        [StringLength(10)]
        public string CteFrete1 { get; set; }
        [Column("DTFaturaFrete1", TypeName = "datetime")]
        public DateTime? DtfaturaFrete1 { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValorFrete2 { get; set; }
        [StringLength(20)]
        public string FaturaFrete2 { get; set; }
        [StringLength(20)]
        public string CteFrete2 { get; set; }
        [Column("DTFaturaFrete2", TypeName = "datetime")]
        public DateTime? DtfaturaFrete2 { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValorExtraFrete { get; set; }
        [Column("DDD")]
        [StringLength(20)]
        public string Ddd { get; set; }
        [StringLength(70)]
        public string Telefone1 { get; set; }
        [StringLength(400)]
        public string Redestinacao { get; set; }
        [StringLength(100)]
        public string AntigoPrefixoRedestinacao { get; set; }
        [StringLength(100)]
        public string AntigoSbRedestinacao { get; set; }
        [StringLength(100)]
        public string AntigoNomeDependenciaRedestinacao { get; set; }
        [StringLength(100)]
        public string AntigoPedidoCompraRedestinacao { get; set; }
        [StringLength(100)]
        public string AntigoProtocoloCdo { get; set; }
        [StringLength(100)]
        public string NovoProtocoloCdo { get; set; }
        [StringLength(100)]
        public string AntigoUfRedestinacao { get; set; }
        [StringLength(100)]
        public string AntigoTipoDependenciaRedestinacao { get; set; }
    }
}
