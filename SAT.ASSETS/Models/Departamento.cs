using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Departamento")]
    public partial class Departamento
    {
        [Key]
        public int CodDepartamento { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeDepartamento { get; set; }
        public byte IndAtivo { get; set; }
    }
}
