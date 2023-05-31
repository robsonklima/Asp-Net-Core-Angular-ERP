using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TarefasStatus")]
    public partial class TarefasStatus
    {
        public TarefasStatus()
        {
            Tarefas = new HashSet<Tarefa>();
        }

        [Key]
        [Column("codTarefaStatus")]
        public int CodTarefaStatus { get; set; }
        [Column("nomeTarefaStatus")]
        [StringLength(50)]
        public string NomeTarefaStatus { get; set; }
        [Column("nomeTarefaStatusAbrev")]
        [StringLength(50)]
        public string NomeTarefaStatusAbrev { get; set; }

        [InverseProperty(nameof(Tarefa.CodTarefaStatusNavigation))]
        public virtual ICollection<Tarefa> Tarefas { get; set; }
    }
}
