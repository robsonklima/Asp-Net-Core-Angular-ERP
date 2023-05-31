using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class TarefasPausa
    {
        [Key]
        [Column("codTarefaPausa")]
        public int CodTarefaPausa { get; set; }
        [Column("codTarefa")]
        public int? CodTarefa { get; set; }
        [Required]
        [Column("descricaoTarefaPausa")]
        [StringLength(500)]
        public string DescricaoTarefaPausa { get; set; }
    }
}
