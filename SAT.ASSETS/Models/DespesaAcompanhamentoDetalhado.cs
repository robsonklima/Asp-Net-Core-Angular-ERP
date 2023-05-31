using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("DespesaAcompanhamentoDetalhado")]
    public partial class DespesaAcompanhamentoDetalhado
    {
        [StringLength(390)]
        public string NomeTecnico { get; set; }
        public int? CodDespesaPeriodo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataInicio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataFim { get; set; }
        public int? CodDespesaPeriodoTecnicoStatus { get; set; }
        [StringLength(310)]
        public string NomeDespesaPeriodoTecnicoStatus { get; set; }
        public int? QtdIndAprovado { get; set; }
        public double? TotalDespesa { get; set; }
        public double? TotalAdiantamento { get; set; }
        public double? RestituirEmpresa { get; set; }
        public double? GastosExcedentes { get; set; }
        public int? IndReprovado { get; set; }
        public int? IndAtivo { get; set; }
    }
}
