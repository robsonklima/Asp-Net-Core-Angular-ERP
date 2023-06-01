using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class TarefasTipo
    {
        [Key]
        [Column("codTarefaTipo")]
        public int CodTarefaTipo { get; set; }
        [Column("nomeTarefaTipo")]
        [StringLength(50)]
        public string NomeTarefaTipo { get; set; }
        [Column("indDesenvolvimento")]
        public int? IndDesenvolvimento { get; set; }
    }
}
