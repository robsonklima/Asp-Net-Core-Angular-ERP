using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("tmp_PecasPendentes2_Filial")]
    public partial class TmpPecasPendentes2Filial
    {
        [StringLength(50)]
        public string NomeFilial { get; set; }
        [StringLength(1)]
        public string Ordem { get; set; }
        [StringLength(50)]
        public string CodMagnus { get; set; }
        [StringLength(150)]
        public string NomePeca { get; set; }
        public int? QtdePecas { get; set; }
        public int? Perc { get; set; }
    }
}
