using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Lingua")]
    public partial class Lingua
    {
        [Key]
        public int CodLingua { get; set; }
        [Required]
        [StringLength(30)]
        public string NomeLingua { get; set; }
        [Required]
        [StringLength(5)]
        public string Cultura { get; set; }
    }
}
