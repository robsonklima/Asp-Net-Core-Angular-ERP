using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("ComparacaoPOS")]
    public partial class ComparacaoPo
    {
        [Column("CNPJ")]
        [StringLength(50)]
        public string Cnpj { get; set; }
        [Column("RAZÃO SOCIAL")]
        [StringLength(150)]
        public string RazãoSocial { get; set; }
        [Column("Total geral")]
        public int? TotalGeral { get; set; }
    }
}
