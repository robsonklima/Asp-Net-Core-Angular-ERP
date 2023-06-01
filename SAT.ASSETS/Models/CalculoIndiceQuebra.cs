using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("CalculoIndiceQuebra")]
    public partial class CalculoIndiceQuebra
    {
        [StringLength(20)]
        public string Pai { get; set; }
        [StringLength(20)]
        public string Magnus { get; set; }
        [StringLength(80)]
        public string DescricaoPeca { get; set; }
        public int? QuantidadePecas { get; set; }
        public int? TotalEquip { get; set; }
        [StringLength(6)]
        public string Mes { get; set; }
    }
}
