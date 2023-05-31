using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("CausasSicredi")]
    public partial class CausasSicredi
    {
        [Required]
        [Column("CodECausa")]
        [StringLength(50)]
        public string CodEcausa { get; set; }
        [Column("NomeCausaSAT")]
        [StringLength(50)]
        public string NomeCausaSat { get; set; }
        [StringLength(50)]
        public string NomeCausaSicredi { get; set; }
        [Column("indAtivo")]
        public int? IndAtivo { get; set; }
    }
}
