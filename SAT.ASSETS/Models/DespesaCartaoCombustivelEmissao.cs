using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("DespesaCartaoCombustivelEmissao")]
    public partial class DespesaCartaoCombustivelEmissao
    {
        [Key]
        public int CodDespesaCartaoCombustivelEmissao { get; set; }
        [StringLength(100)]
        public string Situacao { get; set; }
        public int? CodigoCredito { get; set; }
        public double? ValorSaldo { get; set; }
        public double? ValorCreditoTransferido { get; set; }
        public double? TotalCompras { get; set; }
        [StringLength(30)]
        public string NumeroCartao { get; set; }
        public double? ValorCredito { get; set; }
        public int? CodigoTitulo { get; set; }
        public int? CodigoUsuarioCartao { get; set; }
        [StringLength(50)]
        public string IdentificadorCartao { get; set; }
        [StringLength(50)]
        public string DataValidade { get; set; }
        [StringLength(50)]
        public string DataLiberacaoCredito { get; set; }
        public int? ViaCartao { get; set; }
        [StringLength(50)]
        public string TipoCredito { get; set; }
        public int? CodigoGrupoCredito { get; set; }
        [StringLength(50)]
        public string DataCadastro { get; set; }
        [StringLength(150)]
        public string NomeCompleto { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        [StringLength(50)]
        public string Placa { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
    }
}
