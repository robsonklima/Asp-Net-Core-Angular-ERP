using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("AcompanhamentoTecnico")]
    public partial class AcompanhamentoTecnico
    {
        [Key]
        public int CodAcompanhamentoTecnico { get; set; }
        [Required]
        [StringLength(50)]
        public string CodUsuario { get; set; }
        [Required]
        [StringLength(1000)]
        public string CodTecnico { get; set; }
        [StringLength(1000)]
        public string CodTipoIntervencao { get; set; }
        public int TempoAtualizacao { get; set; }
        public int CodFilial { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
    }
}
