using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("EquipamentoContratoComparacao")]
    public partial class EquipamentoContratoComparacao
    {
        [Key]
        public int CodEquipamentoContratoComparacao { get; set; }
        public int? CodDadoImportacao { get; set; }
        public int? CodEquipContrato { get; set; }
        [StringLength(20)]
        public string NumSerieCliente { get; set; }
        [StringLength(8)]
        public string NumAgenciaCliente { get; set; }
        [Column("NumAgenciaSAT")]
        [StringLength(8)]
        public string NumAgenciaSat { get; set; }
        [Column("DCPostoCliente")]
        [StringLength(2)]
        public string DcpostoCliente { get; set; }
        [Column("DCPostoSAT")]
        [StringLength(2)]
        public string DcpostoSat { get; set; }
        [Column("NomeSLACliente")]
        [StringLength(50)]
        public string NomeSlacliente { get; set; }
        [Column("NomeSLASAT")]
        [StringLength(50)]
        public string NomeSlasat { get; set; }
        [Column("DistanciaKMPATCliente")]
        public int? DistanciaKmpatcliente { get; set; }
        [Column("DistanciaKMPATSAT")]
        public int? DistanciaKmpatsat { get; set; }
        [Column("IndSEMATCliente")]
        public byte? IndSematcliente { get; set; }
        [Column("IndSEMATSAT")]
        public byte? IndSematsat { get; set; }
        public byte? PontoEstrategicoCliente { get; set; }
        [Column("PontoEstrategicoSAT")]
        public byte? PontoEstrategicoSat { get; set; }
        [Column("IndRAcessoCliente")]
        public byte? IndRacessoCliente { get; set; }
        [Column("IndRAcessoSAT")]
        public byte? IndRacessoSat { get; set; }
        [Column("IndRHorarioCliente")]
        public byte? IndRhorarioCliente { get; set; }
        [Column("IndRHorarioSAT")]
        public byte? IndRhorarioSat { get; set; }
        public byte? IndVeraoCliente { get; set; }
        [Column("IndVeraoSAT")]
        public byte? IndVeraoSat { get; set; }
        [Column("IndPAECliente")]
        public byte? IndPaecliente { get; set; }
        [Column("IndPAESAT")]
        public byte? IndPaesat { get; set; }
        public byte? IndAtivoCliente { get; set; }
        [Column("IndAtivoSAT")]
        public byte? IndAtivoSat { get; set; }
        [StringLength(255)]
        public string OutrosCliente { get; set; }
        public byte? IndStatus { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraImport { get; set; }
    }
}
