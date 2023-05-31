using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OrcMotivo")]
    public partial class OrcMotivo
    {
        [Key]
        public int CodOrcMotivo { get; set; }
        [Required]
        [StringLength(255)]
        public string Descricao { get; set; }
    }
}
