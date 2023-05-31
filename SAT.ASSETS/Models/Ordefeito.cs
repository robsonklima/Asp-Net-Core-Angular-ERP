using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ORDefeito")]
    public partial class Ordefeito
    {
        [Key]
        public int CodDefeito { get; set; }
        [StringLength(50)]
        public string Desricao { get; set; }
        public int? CodDefeitoLab { get; set; }
        public int? IndAtivo { get; set; }
    }
}
