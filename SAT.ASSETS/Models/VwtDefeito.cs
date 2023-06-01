using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwtDefeito
    {
        public int CodDefeito { get; set; }
        [Column("CodEDefeito")]
        [StringLength(3)]
        public string CodEdefeito { get; set; }
        [StringLength(50)]
        public string NomeDefeito { get; set; }
        public byte? IndAtivo { get; set; }
        [Required]
        [StringLength(5)]
        public string Culture { get; set; }
    }
}
