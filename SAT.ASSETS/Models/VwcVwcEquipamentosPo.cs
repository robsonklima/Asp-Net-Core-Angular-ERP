using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcVwcEquipamentosPo
    {
        [StringLength(20)]
        public string NumSerie { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeEquip { get; set; }
        [Column("CNPJ")]
        [StringLength(20)]
        public string Cnpj { get; set; }
        [Column("QTDE")]
        public int Qtde { get; set; }
    }
}
