using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("LoBkpTecnico")]
    public partial class LoBkpTecnico
    {
        [Column("IDKITMINIMO")]
        public int Idkitminimo { get; set; }
        [Column("CODTECNICO")]
        public int? Codtecnico { get; set; }
        [Column("CODITEM")]
        [StringLength(50)]
        public string Coditem { get; set; }
        [Column("NUMNF")]
        [StringLength(50)]
        public string Numnf { get; set; }
        [Column("QTDITEM")]
        public int? Qtditem { get; set; }
    }
}
