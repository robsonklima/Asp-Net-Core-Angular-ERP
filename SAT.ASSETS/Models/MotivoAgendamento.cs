using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("MotivoAgendamento")]
    public partial class MotivoAgendamento
    {
        public MotivoAgendamento()
        {
            AgendamentoOs = new HashSet<AgendamentoO>();
        }

        [Key]
        public int CodMotivo { get; set; }
        [StringLength(500)]
        public string DescricaoMotivo { get; set; }
        public byte? IndAtivo { get; set; }
        public byte? IndServico { get; set; }
        [Column("IndAgendamentoBB")]
        public byte? IndAgendamentoBb { get; set; }
        public int? CodTraducao { get; set; }

        [InverseProperty(nameof(AgendamentoO.CodMotivoNavigation))]
        public virtual ICollection<AgendamentoO> AgendamentoOs { get; set; }
    }
}
