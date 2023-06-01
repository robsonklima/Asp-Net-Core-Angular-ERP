using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("TempPecasRat")]
    public partial class TempPecasRat
    {
        [Column("cod_filial")]
        public int? CodFilial { get; set; }
        [Column("cod_tecnico")]
        public int CodTecnico { get; set; }
        [Required]
        [Column("cod_item")]
        [StringLength(20)]
        public string CodItem { get; set; }
        [Column("cod_peca")]
        public int CodPeca { get; set; }
        [Column("nome")]
        [StringLength(50)]
        public string Nome { get; set; }
        [Column("qtd_tot_rat")]
        public int QtdTotRat { get; set; }
    }
}
