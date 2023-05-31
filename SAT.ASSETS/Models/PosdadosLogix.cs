using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("POSDadosLogix")]
    public partial class PosdadosLogix
    {
        [Column("CNPJ_Logix")]
        public double? CnpjLogix { get; set; }
        [Column("Quant_Logix")]
        public double? QuantLogix { get; set; }
        [Column("R$ Logix")]
        public double? RLogix { get; set; }
        [Column("CNPJ_Banrisul")]
        public double? CnpjBanrisul { get; set; }
        [Column("Quant_Banrisul")]
        public double? QuantBanrisul { get; set; }
        public double? Diferença { get; set; }
        [Column("R$ Banrisul")]
        public double? RBanrisul { get; set; }
        [Column("Diferença R$")]
        public double? DiferençaR { get; set; }
        [Column("CNPJ - Logix +")]
        [StringLength(255)]
        public string CnpjLogix1 { get; set; }
        [Column("CRE")]
        public double? Cre { get; set; }
        [Column("Diferença Logix - CRE")]
        public double? DiferençaLogixCre { get; set; }
        [Column("Pendencia financeira")]
        public double? PendenciaFinanceira { get; set; }
        [Column("CRE_Saldo")]
        public double? CreSaldo { get; set; }
        [Column(TypeName = "money")]
        public decimal? Adiantamento { get; set; }
        [Column("CRE_saldo - Adiantamento", TypeName = "money")]
        public decimal? CreSaldoAdiantamento { get; set; }
        [Column("Borba Planilha")]
        public double? BorbaPlanilha { get; set; }
        public double? Abatimento { get; set; }
        [Column("Resultado Final", TypeName = "money")]
        public decimal? ResultadoFinal { get; set; }
        [StringLength(255)]
        public string Status { get; set; }
        [Column("notas fiscais canceladas")]
        public double? NotasFiscaisCanceladas { get; set; }
        public double? F21 { get; set; }
        [Column("NF")]
        [StringLength(255)]
        public string Nf { get; set; }
        [Column("Codigo de cliente", TypeName = "money")]
        public decimal? CodigoDeCliente { get; set; }
        [Column("Nome de cliente")]
        [StringLength(255)]
        public string NomeDeCliente { get; set; }
        [Column("Operção ")]
        [StringLength(255)]
        public string Operção { get; set; }
        [StringLength(255)]
        public string Duplicata { get; set; }
        [Column("Situação de nota fiscal")]
        [StringLength(255)]
        public string SituaçãoDeNotaFiscal { get; set; }
        [StringLength(255)]
        public string Observação { get; set; }
        [Column("cre igual")]
        public double? CreIgual { get; set; }
    }
}
