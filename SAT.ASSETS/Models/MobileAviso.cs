using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("MobileAviso")]
    public partial class MobileAviso
    {
        [Key]
        public int CodMobileAviso { get; set; }
        [Required]
        [StringLength(255)]
        public string Projeto { get; set; }
        [Required]
        [StringLength(255)]
        public string Metodo { get; set; }
        [Required]
        [StringLength(255)]
        public string Descricao { get; set; }
        public byte IndAtivo { get; set; }
    }
}
