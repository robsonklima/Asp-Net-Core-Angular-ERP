using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ORTransporteXFilial")]
    public partial class OrtransporteXfilial
    {
        [Key]
        [Column("CodORTransporteFilial")]
        public int CodOrtransporteFilial { get; set; }
        public int? CodFilial { get; set; }
        public int? CodTransportadora { get; set; }
        public int? IndAtivo { get; set; }
    }
}
