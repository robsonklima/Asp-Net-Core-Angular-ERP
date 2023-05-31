using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwmRelDefeitoPos3VelohW
    {
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Column("DataHoraAberturaOS")]
        public string DataHoraAberturaOs { get; set; }
        [Required]
        public string DataHoraFechamento { get; set; }
        [Column("Nome_Local")]
        [StringLength(50)]
        public string NomeLocal { get; set; }
        [Required]
        [StringLength(20)]
        public string NumSerieRetirado { get; set; }
        [Column("Num_Serie")]
        [StringLength(20)]
        public string NumSerie { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Abertura { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeDefeito { get; set; }
    }
}
