using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwExportacaoInstalaco
    {
        public int Código { get; set; }
        [StringLength(50)]
        public string Filial { get; set; }
        [StringLength(50)]
        public string Autorizada { get; set; }
        [StringLength(50)]
        public string Região { get; set; }
        [Column("SLA")]
        [StringLength(50)]
        public string Sla { get; set; }
        [Required]
        [StringLength(50)]
        public string Lote { get; set; }
        [Column("Data_Recebimento_Lote", TypeName = "datetime")]
        public DateTime DataRecebimentoLote { get; set; }
        [Required]
        [StringLength(50)]
        public string Modelo { get; set; }
        [Column("Nº_Série")]
        [StringLength(20)]
        public string NºSérie { get; set; }
        [Column("Nº_Bem")]
        [StringLength(20)]
        public string NºBem { get; set; }
        [Required]
        [StringLength(8)]
        public string Agência { get; set; }
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
        [StringLength(8)]
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
        [StringLength(8)]
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
        [Required]
        [StringLength(20)]
        public string CodUsuarioBlock { get; set; }
    }
}
