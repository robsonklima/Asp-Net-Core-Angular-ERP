using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("ObservacaoFaturamento")]
    public partial class ObservacaoFaturamento
    {
        public int? CodFaturamento { get; set; }
        public int CodModalidadeObsFaturamento { get; set; }
        public string Observacao { get; set; }
        [Required]
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        public int? IndAtivo { get; set; }

        [ForeignKey(nameof(CodFaturamento))]
        public virtual FaturamentoContratoControle CodFaturamentoNavigation { get; set; }
    }
}
