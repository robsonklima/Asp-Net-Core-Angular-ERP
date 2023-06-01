using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TarefaImpactoGrupo")]
    public partial class TarefaImpactoGrupo
    {
        [Key]
        [Column("codTarefaImpactoGrupo")]
        public int CodTarefaImpactoGrupo { get; set; }
        [Column("codTarefaImpacto")]
        [StringLength(10)]
        public string CodTarefaImpacto { get; set; }
        [StringLength(50)]
        public string NomeTarefaImpactoGrupo { get; set; }
        [Column("indAtivo")]
        public int? IndAtivo { get; set; }
        [Column("indResolucaoSAT")]
        public int IndResolucaoSat { get; set; }
        [Column("ordem")]
        public int? Ordem { get; set; }
    }
}
