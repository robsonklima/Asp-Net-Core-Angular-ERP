using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcCalculaIndiceQuebra2
    {
        public int Filial { get; set; }
        [StringLength(20)]
        public string Pai { get; set; }
        [StringLength(24)]
        public string Magnus { get; set; }
        [Column("Descrição Peça")]
        [StringLength(80)]
        public string DescriçãoPeça { get; set; }
        [Column("Quantidade Peças")]
        public int? QuantidadePeças { get; set; }
        public int? TotalEquip { get; set; }
    }
}
