using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ORDestino")]
    public partial class Ordestino
    {
        [Key]
        public int CodDestino { get; set; }
        [Required]
        [StringLength(30)]
        public string Destino { get; set; }
    }
}
