using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class TarefasOcorrencia
    {
        [Key]
        [Column("codTarefaOcorrencia")]
        public int CodTarefaOcorrencia { get; set; }
        [Column("codTarefa")]
        public int CodTarefa { get; set; }
        [Column("codTarefaStatus")]
        public int CodTarefaStatus { get; set; }
        [Required]
        [Column("codUsuario")]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        [Required]
        [Column("descricaoTarefaOcorrencia")]
        [StringLength(500)]
        public string DescricaoTarefaOcorrencia { get; set; }
        [Column("dataHoraTarefaOcorrencia", TypeName = "datetime")]
        public DateTime DataHoraTarefaOcorrencia { get; set; }
    }
}
