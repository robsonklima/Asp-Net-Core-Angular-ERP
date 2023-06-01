using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("LocaisBanrisul")]
    public partial class LocaisBanrisul
    {
        [StringLength(255)]
        public string NomeLocal { get; set; }
        [Column("CNPJ")]
        public double? Cnpj { get; set; }
        [StringLength(255)]
        public string F3 { get; set; }
    }
}
