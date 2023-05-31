using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("POSCRENEW")]
    public partial class Poscrenew
    {
        [Column("cnpj")]
        public double? Cnpj { get; set; }
        [Column("valor")]
        [StringLength(255)]
        public string Valor { get; set; }
    }
}
