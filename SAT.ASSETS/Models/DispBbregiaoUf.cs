using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("DispBBRegiaoUF")]
    public partial class DispBbregiaoUf
    {
        [Column("CodDispBBRegiaoUF")]
        public int CodDispBbregiaoUf { get; set; }
        [Column("CodDispBBRegiao")]
        [StringLength(10)]
        public string CodDispBbregiao { get; set; }
        [Column("CodUF")]
        public int CodUf { get; set; }
        public int IndAtivo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [Required]
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
    }
}
