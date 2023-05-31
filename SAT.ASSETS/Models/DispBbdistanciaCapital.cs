using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("DispBBDistanciaCapital")]
    public partial class DispBbdistanciaCapital
    {
        [Column("CodDispBBDistanciaCapital")]
        public int CodDispBbdistanciaCapital { get; set; }
        public int CodCidade { get; set; }
        public int Distancia { get; set; }
        [Required]
        [StringLength(50)]
        public string Municipio { get; set; }
        [Required]
        [Column("UF")]
        [StringLength(2)]
        public string Uf { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [Required]
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
    }
}
