using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("OrcamentosFaturamento")]
    public partial class OrcamentosFaturamento
    {
        public double? CodOrcamentoFaturamento { get; set; }
        public double? CodOrcamento { get; set; }
        [StringLength(255)]
        public string CodClienteBancada { get; set; }
        public double? CodFilial { get; set; }
        [Column("NumOSPerto")]
        public double? NumOsperto { get; set; }
        [StringLength(255)]
        public string NumOrcamento { get; set; }
        [StringLength(255)]
        public string DescricaoNotaFiscal { get; set; }
        [StringLength(255)]
        public string ValorPeca { get; set; }
        public double? QtdePeca { get; set; }
        [StringLength(255)]
        public string ValorServico { get; set; }
        [Column("NumNF")]
        public double? NumNf { get; set; }
        [Column("DataEmissaoNF")]
        [StringLength(255)]
        public string DataEmissaoNf { get; set; }
        public double? IndFaturado { get; set; }
        [StringLength(255)]
        public string IndRegistroDanfe { get; set; }
        [StringLength(255)]
        public string CaminhoDanfe { get; set; }
        [StringLength(255)]
        public string CodUsuarioCad { get; set; }
        [StringLength(255)]
        public string DataHoraCad { get; set; }
    }
}
