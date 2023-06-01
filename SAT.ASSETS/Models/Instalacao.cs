using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Instalacao")]
    public partial class Instalacao
    {
        public Instalacao()
        {
            InstalAnexos = new HashSet<InstalAnexo>();
            InstalObs = new HashSet<InstalOb>();
            InstalOs = new HashSet<InstalO>();
            InstalPagtoInstals = new HashSet<InstalPagtoInstal>();
            InstalPleitoInstals = new HashSet<InstalPleitoInstal>();
            InstalRessalvas = new HashSet<InstalRessalva>();
        }

        [Key]
        public int CodInstalacao { get; set; }
        public int? CodInstalLote { get; set; }
        public int CodContrato { get; set; }
        public int CodTipoEquip { get; set; }
        public int CodGrupoEquip { get; set; }
        public int? CodEquip { get; set; }
        public int? CodRegiao { get; set; }
        public int? CodAutorizada { get; set; }
        public int? CodFilial { get; set; }
        [Column("CodSLA")]
        public int? CodSla { get; set; }
        public int? CodEquipContrato { get; set; }
        public int CodCliente { get; set; }
        public int? CodPosto { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataSugEntrega { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataConfEntrega { get; set; }
        [Column("DataRecDM", TypeName = "datetime")]
        public DateTime? DataRecDm { get; set; }
        [Column("NFRemessa")]
        [StringLength(20)]
        public string Nfremessa { get; set; }
        [Column("DataNFRemessa", TypeName = "datetime")]
        public DateTime? DataNfremessa { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataExpedicao { get; set; }
        public int? CodTransportadora { get; set; }
        public int? CodClienteEnt { get; set; }
        public int? CodPostoEnt { get; set; }
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
        public DateTime? DataSugInstalacao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataConfInstalacao { get; set; }
        [Column("CodOS")]
        public int? CodOs { get; set; }
        [Column("CodRAT")]
        public int? CodRat { get; set; }
        public int? CodClienteIns { get; set; }
        public int? CodPostoIns { get; set; }
        [Column("DataBI", TypeName = "datetime")]
        public DateTime? DataBi { get; set; }
        [Column("QtdParaboldBI")]
        public int? QtdParaboldBi { get; set; }
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
        [Column("IndRATOK")]
        public byte? IndRatok { get; set; }
        [Column("IndLaudoOK")]
        public byte? IndLaudoOk { get; set; }
        [Column("IndRE5330OK")]
        public byte? IndRe5330ok { get; set; }
        [Column("CodInstalNFVenda")]
        public int? CodInstalNfvenda { get; set; }
        [Column("NFVenda_DEL")]
        [StringLength(20)]
        public string NfvendaDel { get; set; }
        [Column("DataNFVenda_DEL", TypeName = "datetime")]
        public DateTime? DataNfvendaDel { get; set; }
        [Column("DataNFVendaEnvio_DEL", TypeName = "datetime")]
        public DateTime? DataNfvendaEnvioDel { get; set; }
        [Column("CodInstalNFAut")]
        public int? CodInstalNfaut { get; set; }
        [Column("VlrPagtoNFAut", TypeName = "decimal(10, 2)")]
        public decimal? VlrPagtoNfaut { get; set; }
        [StringLength(10)]
        public string NumFaturaTransp { get; set; }
        public int CodInstalStatus { get; set; }
        [StringLength(20)]
        public string CodUsuarioBlock { get; set; }
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
        public string NomeTransportadora { get; set; }
        [StringLength(50)]
        public string NomeReponsavelBancoAcompanhamento { get; set; }
        [StringLength(20)]
        public string NumMatriculaRespBancoAcompanhamento { get; set; }
        [Column("vlrKMTradeIn1", TypeName = "decimal(10, 2)")]
        public decimal? VlrKmtradeIn1 { get; set; }
        [Column("vlrKMTradeIn2", TypeName = "decimal(10, 2)")]
        public decimal? VlrKmtradeIn2 { get; set; }
        [Column("vlrDesFixacao1", TypeName = "decimal(10, 2)")]
        public decimal? VlrDesFixacao1 { get; set; }
        [Column("vlrDesFixacao2", TypeName = "decimal(10, 2)")]
        public decimal? VlrDesFixacao2 { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? DistanciaKmTradeIn1 { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? DistanciaKmTradeIn2 { get; set; }
        [Column("NFTransportadoraTradeIn")]
        [StringLength(20)]
        public string NftransportadoraTradeIn { get; set; }
        [Column("DataNFTransportadoraTradeIn", TypeName = "datetime")]
        public DateTime? DataNftransportadoraTradeIn { get; set; }
        [Column("vlrRecolhimentoTradeIn", TypeName = "decimal(10, 2)")]
        public decimal? VlrRecolhimentoTradeIn { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        [StringLength(80)]
        public string FornecedorCompraTradeIn { get; set; }
        [Column("NFVendaTradeIn")]
        [StringLength(20)]
        public string NfvendaTradeIn { get; set; }
        [Column("DataNFVendaTradeIn", TypeName = "datetime")]
        public DateTime? DataNfvendaTradeIn { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValorUnitarioVenda { get; set; }
        [StringLength(30)]
        public string Romaneio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DtPrevRecolhimentoTradeIn { get; set; }
        [Column("NFRemessaConferida")]
        [StringLength(50)]
        public string NfremessaConferida { get; set; }
        [StringLength(20)]
        public string DtbCliente { get; set; }
        [StringLength(20)]
        public string FaturaTranspReEntrega { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DtReEntrega { get; set; }
        [StringLength(50)]
        public string ResponsavelRecebReEntrega { get; set; }
        public byte? IndInstalacao { get; set; }
        [StringLength(50)]
        public string PedidoCompra { get; set; }
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
        public string AntigoUfRedestinacao { get; set; }
        [StringLength(100)]
        public string AntigoTipoDependenciaRedestinacao { get; set; }
        [StringLength(100)]
        public string AntigoPedidoCompraRedestinacao { get; set; }
        [StringLength(100)]
        public string AntigoProtocoloCdo { get; set; }
        [StringLength(100)]
        public string NovoProtocoloCdo { get; set; }

        [InverseProperty(nameof(InstalAnexo.CodInstalacaoNavigation))]
        public virtual ICollection<InstalAnexo> InstalAnexos { get; set; }
        [InverseProperty(nameof(InstalOb.CodInstalacaoNavigation))]
        public virtual ICollection<InstalOb> InstalObs { get; set; }
        [InverseProperty(nameof(InstalO.CodInstalacaoNavigation))]
        public virtual ICollection<InstalO> InstalOs { get; set; }
        [InverseProperty(nameof(InstalPagtoInstal.CodInstalacaoNavigation))]
        public virtual ICollection<InstalPagtoInstal> InstalPagtoInstals { get; set; }
        [InverseProperty(nameof(InstalPleitoInstal.CodInstalacaoNavigation))]
        public virtual ICollection<InstalPleitoInstal> InstalPleitoInstals { get; set; }
        [InverseProperty(nameof(InstalRessalva.CodInstalacaoNavigation))]
        public virtual ICollection<InstalRessalva> InstalRessalvas { get; set; }
    }
}
