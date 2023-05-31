using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("Tmp_Calcula_Indice_Quebra_Por_Cliente")]
    public partial class TmpCalculaIndiceQuebraPorCliente
    {
        public int Filial { get; set; }
        public int Regiao { get; set; }
        [StringLength(20)]
        public string Pai { get; set; }
        [StringLength(20)]
        public string MagnusPeca { get; set; }
        [StringLength(80)]
        public string DescricaoPeca { get; set; }
        [Column("QTDPeca")]
        public int? Qtdpeca { get; set; }
        public int? CodCliente { get; set; }
        public int? CodEquip { get; set; }
    }
}
