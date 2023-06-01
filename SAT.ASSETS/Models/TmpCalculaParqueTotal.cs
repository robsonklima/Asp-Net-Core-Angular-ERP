using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("Tmp_Calcula_Parque_Total")]
    public partial class TmpCalculaParqueTotal
    {
        [StringLength(20)]
        public string Pai { get; set; }
        [Column("QTD")]
        public int? Qtd { get; set; }
    }
}
