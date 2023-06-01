using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ORSolucao")]
    public partial class Orsolucao
    {
        [Key]
        public int CodSolucao { get; set; }
        [StringLength(50)]
        public string Desricao { get; set; }
        public int? CodSolucaoLab { get; set; }
        public int? IndAtivo { get; set; }
    }
}
