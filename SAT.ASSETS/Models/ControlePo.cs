using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ControlePOS")]
    public partial class ControlePo
    {
        [Key]
        [Column("CodControlePOS")]
        public int CodControlePos { get; set; }
        [Column("CodOS")]
        public int? CodOs { get; set; }
        [Column("CNPJEstabelecimentoCliente")]
        [StringLength(30)]
        public string CnpjestabelecimentoCliente { get; set; }
        [StringLength(35)]
        public string NumTerminal { get; set; }
        public int? CodEquip { get; set; }
        public int? CodEquipContrato { get; set; }
        public byte? IndVeloh { get; set; }
        public byte? IndAtivo { get; set; }
        public byte? IndSubstituicao { get; set; }
    }
}
