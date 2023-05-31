using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ORNivelCheckList")]
    public partial class OrnivelCheckList
    {
        [Key]
        public int CodNivel { get; set; }
        [StringLength(20)]
        public string Descricao { get; set; }
    }
}
