using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("AjudaTopico")]
    public partial class AjudaTopico
    {
        [Key]
        public int CodAjudaTopico { get; set; }
        [StringLength(150)]
        public string Titulo { get; set; }
        [StringLength(150)]
        public string Enunciado { get; set; }
        [StringLength(1000)]
        public string Texto { get; set; }
    }
}
