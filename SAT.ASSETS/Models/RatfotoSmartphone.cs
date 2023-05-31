using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("RATFotoSmartphone")]
    public partial class RatfotoSmartphone
    {
        [Key]
        [Column("CodRATFotoSmartphone")]
        public int CodRatfotoSmartphone { get; set; }
        [Column("CodOS")]
        public int? CodOs { get; set; }
        [Column("NumRAT")]
        [StringLength(12)]
        public string NumRat { get; set; }
        [StringLength(150)]
        public string NomeFoto { get; set; }
        [StringLength(50)]
        public string Modalidade { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
    }
}
