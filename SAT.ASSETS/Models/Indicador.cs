using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Indicador")]
    public partial class Indicador
    {
        [Key]
        [StringLength(10)]
        public string CodIndicador { get; set; }
        [Column("Indicador")]
        public byte Indicador1 { get; set; }
        [Required]
        [StringLength(2)]
        public string Sigla { get; set; }
        [StringLength(30)]
        public string DescM { get; set; }
        [Column("DescMPlural")]
        [StringLength(30)]
        public string DescMplural { get; set; }
        [StringLength(30)]
        public string DescF { get; set; }
        [Column("DescFPlural")]
        [StringLength(30)]
        public string DescFplural { get; set; }
        [Required]
        [StringLength(30)]
        public string Source { get; set; }
    }
}
