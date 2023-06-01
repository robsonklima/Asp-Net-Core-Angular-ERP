using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcConsAdiantRd
    {
        public int CodTecnico { get; set; }
        [StringLength(50)]
        public string Tecnico { get; set; }
        [Column("NroRD")]
        public int NroRd { get; set; }
        [StringLength(4000)]
        public string DataInicio { get; set; }
        [StringLength(4000)]
        public string DataFim { get; set; }
        [Column("TotalRD", TypeName = "decimal(38, 2)")]
        public decimal? TotalRd { get; set; }
        [Column(TypeName = "decimal(38, 2)")]
        public decimal? Despesas { get; set; }
        [Column(TypeName = "decimal(38, 2)")]
        public decimal? Adiantamento { get; set; }
        public int Reembolso { get; set; }
        [Column("SaldoAdiantamentoSAT", TypeName = "decimal(38, 2)")]
        public decimal SaldoAdiantamentoSat { get; set; }
        [StringLength(30)]
        public string Protocolo { get; set; }
        [StringLength(4000)]
        public string DtEnvioProtocolo { get; set; }
        public int? Controladoria { get; set; }
        public int? Contabilidade { get; set; }
        [Required]
        [StringLength(7)]
        public string Situacao { get; set; }
        public int CodDespesaPeriodoTecnicoStatus { get; set; }
        [StringLength(60)]
        public string AnoMes { get; set; }
    }
}
