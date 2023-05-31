using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwtMenu
    {
        public int CodMenu { get; set; }
        public int? CodMenuPai { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeMenu { get; set; }
        public int Nivel { get; set; }
        public int? OrdemClassif { get; set; }
        [StringLength(100)]
        public string Funcao { get; set; }
        public byte IndSmartCard { get; set; }
        public byte? IndMobile { get; set; }
        public byte IndAtivo { get; set; }
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        [Required]
        [StringLength(5)]
        public string Culture { get; set; }
    }
}
