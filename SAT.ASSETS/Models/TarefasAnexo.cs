using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class TarefasAnexo
    {
        [Key]
        [Column("codTarefaAnexo")]
        public int CodTarefaAnexo { get; set; }
        [Column("codTarefa")]
        public int CodTarefa { get; set; }
        [Required]
        [Column("nomeArquivo")]
        [StringLength(100)]
        public string NomeArquivo { get; set; }
        [Required]
        [Column("nomeTarefaAnexo")]
        [StringLength(50)]
        public string NomeTarefaAnexo { get; set; }
        [Column("descricaoTarefaAnexo")]
        [StringLength(500)]
        public string DescricaoTarefaAnexo { get; set; }

        [ForeignKey(nameof(CodTarefa))]
        [InverseProperty(nameof(Tarefa.TarefasAnexos))]
        public virtual Tarefa CodTarefaNavigation { get; set; }
    }
}
