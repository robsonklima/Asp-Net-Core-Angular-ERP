using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("POS_Dev")]
    public partial class PosDev
    {
        [Column("CNPJ")]
        public double? Cnpj { get; set; }
        [StringLength(255)]
        public string Codigo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Data { get; set; }
        [Column("NF")]
        [StringLength(255)]
        public string Nf { get; set; }
        [StringLength(255)]
        public string C1 { get; set; }
        [StringLength(255)]
        public string C2 { get; set; }
    }
}
