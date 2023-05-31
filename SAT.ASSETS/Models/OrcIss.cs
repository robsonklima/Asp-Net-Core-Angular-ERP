using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OrcIss")]
    public partial class OrcIss
    {
        [Key]
        public int CodOrcIss { get; set; }
        public int CodigoFilial { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal Valor { get; set; }
    }
}
