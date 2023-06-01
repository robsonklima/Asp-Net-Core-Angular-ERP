using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class DboVwTarefa
    {
        [Required]
        [Column("tituloTarefa")]
        public string TituloTarefa { get; set; }
        [Column("descricaoTarefa")]
        public string DescricaoTarefa { get; set; }
        [Column("nomeTarefaStatus")]
        [StringLength(50)]
        public string NomeTarefaStatus { get; set; }
        [Column("dataHoraCadastro", TypeName = "datetime")]
        public DateTime DataHoraCadastro { get; set; }
    }
}
