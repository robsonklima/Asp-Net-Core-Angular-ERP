using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcIndAtivo
    {
        public byte Indicador { get; set; }
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
