using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("temp")]
    public partial class Temp
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("nome")]
        [StringLength(50)]
        public string Nome { get; set; }
        [Column("endereco")]
        [StringLength(200)]
        public string Endereco { get; set; }
    }
}
