using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("POSCRE")]
    public partial class Poscre1
    {
        [Column("CNPJ")]
        [StringLength(255)]
        public string Cnpj { get; set; }
        [Column("VALOR")]
        public double? Valor { get; set; }
    }
}
