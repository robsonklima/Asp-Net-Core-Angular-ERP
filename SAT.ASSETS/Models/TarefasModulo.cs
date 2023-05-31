using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class TarefasModulo
    {
        public TarefasModulo()
        {
            Tarefas = new HashSet<Tarefa>();
        }

        [Key]
        [Column("codTarefaModulo")]
        public int CodTarefaModulo { get; set; }
        [Required]
        [Column("nomeTarefaModulo")]
        [StringLength(50)]
        public string NomeTarefaModulo { get; set; }
        [Column("indAtivo")]
        public int IndAtivo { get; set; }
        [Column("nomeTarefaModuloAbrev")]
        [StringLength(50)]
        public string NomeTarefaModuloAbrev { get; set; }

        [InverseProperty(nameof(Tarefa.CodTarefaModuloNavigation))]
        public virtual ICollection<Tarefa> Tarefas { get; set; }
    }
}
