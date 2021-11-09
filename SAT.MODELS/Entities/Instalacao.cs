using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities{
    public class Instalacao
    {
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
        public int? CodSla { get; set; }
        public int? CodEquipContrato { get; set; }
        public int CodCliente { get; set; }
        public int? CodPosto { get; set; }
        public DateTime? DataSugEntrega { get; set; }
        public DateTime? DataConfEntrega { get; set; }
        public DateTime? DataRecDm { get; set; }
        public string Nfremessa { get; set; }
        public DateTime? DataNfremessa { get; set; }
        public DateTime? DataExpedicao { get; set; }
        public int? CodTransportadora { get; set; }
        public int? CodClienteEnt { get; set; }
        public int? CodPostoEnt { get; set; }
        public DateTime? DataHoraChegTranspBt { get; set; }
        public byte? IndEquipPosicOkbt { get; set; }
        public string NomeRespBancoBt { get; set; }
        public string NumMatriculaBt { get; set; }
        public byte? IndBtorigEnt { get; set; }
        public byte? IndBtok { get; set; }
        public DateTime? DataSugInstalacao { get; set; }
        public DateTime? DataConfInstalacao { get; set; }
        public int? CodOs { get; set; }
        public int? CodRat { get; set; }
        public int? CodClienteIns { get; set; }
        public int? CodPostoIns { get; set; }
        public DateTime? DataBi { get; set; }
        public int? QtdParaboldBi { get; set; }
        public string SuperE { get; set; }
        public string Csl { get; set; }
        public string CSOServ { get; set; }
        public string Supridora { get; set; }
        public string MST606TipoNovo { get; set; }
        public byte? IndEquipRebaixadoBI { get; set; }
        public string NomeRespBancoBI { get; set; }
        public string NumMatriculaBI { get; set; }
        public byte? IndBiorigEnt { get; set; }
        public byte? IndBIOK { get; set; }
        public byte? IndRATOK { get; set; }
        public byte? IndLaudoOK { get; set; }
        public byte? IndRE5330OK { get; set; }
        public int? CodInstalNFVenda { get; set; }
        public string NFVenda_DEL { get; set; }
        public DateTime? DataNFVenda_DEL { get; set; }
        public DateTime? DataNFVendaEnvio_DEL { get; set; }
        public int? CodInstalNFAut { get; set; }
        public decimal? VlrPagtoNFAut { get; set; }
        public string NumFaturaTransp { get; set; }
        public int CodInstalStatus { get; set; }
        public string CodUsuarioBlock { get; set; }
        public byte? TermoDescaracterizacao { get; set; }
        public string FornecedorTradeIn1 { get; set; }
        public string FornecedorTradeIn2 { get; set; }
        public string BemTradeIn { get; set; }
        public string Fabricante { get; set; }
        public string Modelo { get; set; }
        public DateTime? DataRetirada { get; set; }
        public string NFTradeIn1 { get; set; }
        public string NFTradeIn2 { get; set; }
        public DateTime? DataNFTradeIn1 { get; set; }
        public DateTime? DataNFTradeIn2 { get; set; }
        public string NomeTransportadora { get; set; }
        public string NomeReponsavelBancoAcompanhamento { get; set; }
        public string NumMatriculaRespBancoAcompanhamento { get; set; }
        public decimal? vlrKMTradeIn1 { get; set; }
        public decimal? vlrKMTradeIn2 { get; set; }
        public decimal? VlrDesFixacao1 { get; set; }
        public decimal? VlrDesFixacao2 { get; set; }
        public decimal? DistanciaKmTradeIn1 { get; set; }
        public decimal? DistanciaKmTradeIn2 { get; set; }
        public string NFTransportadoraTradeIn { get; set; }
        public DateTime? DataNFTransportadoraTradeIn { get; set; }
        public decimal? VlrRecolhimentoTradeIn { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public string FornecedorCompraTradeIn { get; set; }
        public string NFVendaTradeIn { get; set; }
        public DateTime? DataNFVendaTradeIn { get; set; }
        public decimal? ValorUnitarioVenda { get; set; }
        public string Romaneio { get; set; }
        public DateTime? DtPrevRecolhimentoTradeIn { get; set; }
        public string NFRemessaConferida { get; set; }
        public string DtbCliente { get; set; }
        public string FaturaTranspReEntrega { get; set; }
        public DateTime? DtReEntrega { get; set; }
        public string ResponsavelRecebReEntrega { get; set; }
        public byte? IndInstalacao { get; set; }
        public string PedidoCompra { get; set; }
        public DateTime? DTVencBord100 { get; set; }
        public DateTime? DTEntBord100 { get; set; }
        public DateTime? DTVencBord90 { get; set; }
        public DateTime? DTEntBord90 { get; set; }
        public DateTime? DTVencBord10 { get; set; }
        public DateTime? DTEntBord10 { get; set; }
        public decimal? ValorFrete1 { get; set; }
        public string FaturaFrete1 { get; set; }
        public string CteFrete1 { get; set; }
        public DateTime? DTFaturaFrete1 { get; set; }
        public decimal? ValorFrete2 { get; set; }
        public string FaturaFrete2 { get; set; }
        public string CteFrete2 { get; set; }
        public DateTime? DTFaturaFrete2 { get; set; }
        public decimal? ValorExtraFrete { get; set; }
        public string Ddd { get; set; }
        public string Telefone1 { get; set; }
        public string Redestinacao { get; set; }
        public string AntigoPrefixoRedestinacao { get; set; }
        public string AntigoSbRedestinacao { get; set; }
        public string AntigoNomeDependenciaRedestinacao { get; set; }
        public string AntigoUfRedestinacao { get; set; }
        public string AntigoTipoDependenciaRedestinacao { get; set; }
        public string AntigoPedidoCompraRedestinacao { get; set; }
        public string AntigoProtocoloCdo { get; set; }
        public string NovoProtocoloCdo { get; set; }

        [ForeignKey("CodInstalLote")]
        public InstalLote InstalLote { get; set; }

        [ForeignKey("CodContrato")]
        public Contrato Contrato { get; set; }

        [ForeignKey("CodTipoEquip")]
        public TipoEquipamento TipoEquipamento { get; set; }    

        [ForeignKey("CodGrupoEquip")]
        public GrupoEquipamento GrupoEquipamento { get; set; }         

        [ForeignKey("CodEquip")]
        public Equipamento Equipamento { get; set; }         

        [ForeignKey("CodRegiao")]
        public Regiao Regiao { get; set; }         

        [ForeignKey("CodAutorizada")]
        public Autorizada Autorizada { get; set; }           

        [ForeignKey("CodFilial")]
        public Filial Filial { get; set; }  

        [ForeignKey("CodEquipContrato")]
        public EquipamentoContrato EquipamentoContrato { get; set; }          

        [ForeignKey("CodCliente")]
        public Cliente Cliente { get; set; }       

        [ForeignKey("CodPosto")]
        public LocalAtendimento LocalAtendimento { get; set; }                       

        //[ForeignKey("CodSla")]
        //public SLA SLA { get; set; }                 
    }    
}