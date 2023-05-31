using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("LaudoSituacao")]
    public partial class LaudoSituacao
    {
        [Key]
        public int CodLaudoSituacao { get; set; }
        public int CodLaudo { get; set; }
        [Required]
        [StringLength(1000)]
        public string Causa { get; set; }
        [Required]
        [StringLength(1000)]
        public string Acao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
    }
}
