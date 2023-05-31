using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Localizacao")]
    public partial class Localizacao
    {
        [Key]
        public int CodLocalizacao { get; set; }
        [Required]
        [StringLength(50)]
        public string Latitude { get; set; }
        [Required]
        [StringLength(50)]
        public string Longitude { get; set; }
        [Required]
        [StringLength(50)]
        public string CodUsuario { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
    }
}
