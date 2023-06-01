using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ORTransporte")]
    public partial class Ortransporte
    {
        [Key]
        public int CodTransportadora { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeTransportadora { get; set; }
        public int? IndAtivo { get; set; }
        public int? CodModal { get; set; }
    }
}
