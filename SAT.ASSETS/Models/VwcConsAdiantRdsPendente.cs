using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcConsAdiantRdsPendente
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
        [StringLength(4000)]
        public string Controladoria { get; set; }
        [Required]
        [StringLength(40)]
        public string Situacao { get; set; }
        [Required]
        [StringLength(7)]
        public string Cor { get; set; }
        [Required]
        [StringLength(30)]
        public string NomeDespesaPeriodoTecnicoStatus { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataInicio2 { get; set; }
    }
}
