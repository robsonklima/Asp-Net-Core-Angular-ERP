using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class TarefasObjetosAlterado
    {
        [Key]
        [Column("codTarefaObjetoAlterado")]
        public int CodTarefaObjetoAlterado { get; set; }
        [Column("codTarefa")]
        public int CodTarefa { get; set; }
        [Required]
        [Column("nomeObjetoAlterado")]
        [StringLength(100)]
        public string NomeObjetoAlterado { get; set; }
        [Column("descricaoObjetoAlterado")]
        [StringLength(300)]
        public string DescricaoObjetoAlterado { get; set; }
    }
}
