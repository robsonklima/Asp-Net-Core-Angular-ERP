using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TarefaNotificacao")]
    public partial class TarefaNotificacao
    {
        [Key]
        public int CodTarefaNotificacao { get; set; }
        public int CodTarefa { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        public string Descricao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }

        [ForeignKey(nameof(CodTarefa))]
        [InverseProperty(nameof(Tarefa.TarefaNotificacaos))]
        public virtual Tarefa CodTarefaNavigation { get; set; }
        [ForeignKey(nameof(CodUsuario))]
        [InverseProperty(nameof(Usuario.TarefaNotificacaos))]
        public virtual Usuario CodUsuarioNavigation { get; set; }
    }
}
