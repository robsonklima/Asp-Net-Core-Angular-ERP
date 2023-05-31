using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Cargo")]
    public partial class Cargo
    {
        [Key]
        public int CodCargo { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeCargo { get; set; }
        public byte IndAtivo { get; set; }
    }
}
