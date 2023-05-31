using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("DeParaCausaMotivo")]
    public partial class DeParaCausaMotivo
    {
        public int CodDeParaCausaMotivo { get; set; }
        public int CodCausa { get; set; }
        [Required]
        [Column("CodECausa")]
        [StringLength(5)]
        public string CodEcausa { get; set; }
        [Required]
        [StringLength(70)]
        public string NomeCausa { get; set; }
        [Required]
        [StringLength(3)]
        public string CodMotivo { get; set; }
    }
}
