using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TicketLogCredito")]
    [Index(nameof(NumeroCartao), nameof(DataCadastro), nameof(ValorCredito), nameof(CodigoCredito), Name = "UQ_TicketLogCredito_entries", IsUnique = true)]
    public partial class TicketLogCredito
    {
        [Key]
        public int CodTicketLogCredito { get; set; }
        [Required]
        [Column("situacao")]
        [StringLength(120)]
        public string Situacao { get; set; }
        [Required]
        [Column("codigoCredito")]
        [StringLength(50)]
        public string CodigoCredito { get; set; }
        [Column("valorSaldo")]
        public double ValorSaldo { get; set; }
        [Column("valorCreditoTransferido")]
        public double ValorCreditoTransferido { get; set; }
        [Column("totalCompras")]
        public double TotalCompras { get; set; }
        [Required]
        [Column("numeroCartao")]
        [StringLength(50)]
        public string NumeroCartao { get; set; }
        [Column("valorCredito")]
        public double ValorCredito { get; set; }
        [Column("codigoTitulo")]
        public int CodigoTitulo { get; set; }
        [Column("codigoUsuarioCartao")]
        public int CodigoUsuarioCartao { get; set; }
        [Required]
        [Column("identificadorCartao")]
        [StringLength(100)]
        public string IdentificadorCartao { get; set; }
        [Required]
        [Column("justificativa")]
        [StringLength(200)]
        public string Justificativa { get; set; }
        [Column("dataLiberacaoCredito", TypeName = "date")]
        public DateTime DataLiberacaoCredito { get; set; }
        [Column("viaCartao")]
        public int ViaCartao { get; set; }
        [Required]
        [Column("tipoCredito")]
        [StringLength(100)]
        public string TipoCredito { get; set; }
        [Column("codigoGrupoCredito")]
        public int CodigoGrupoCredito { get; set; }
        [Column("dataCadastro", TypeName = "date")]
        public DateTime DataCadastro { get; set; }
        [Required]
        [Column("nomeCompleto")]
        [StringLength(200)]
        public string NomeCompleto { get; set; }
        [Required]
        [Column("status")]
        [StringLength(80)]
        public string Status { get; set; }
        [Required]
        [Column("placa")]
        [StringLength(20)]
        public string Placa { get; set; }
        [Column("dataHoraConsulta", TypeName = "datetime")]
        public DateTime? DataHoraConsulta { get; set; }
    }
}
