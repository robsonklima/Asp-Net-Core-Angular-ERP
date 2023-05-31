using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OSIntervencaoEquipamento")]
    public partial class OsintervencaoEquipamento
    {
        [Key]
        [Column("CodOSIntervencaoEquipamento")]
        public int CodOsintervencaoEquipamento { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [StringLength(30)]
        public string NumAtestado { get; set; }
        [StringLength(30)]
        public string NumLacreRetirado { get; set; }
        [StringLength(30)]
        public string NumLacreColocado { get; set; }
        [StringLength(30)]
        public string NumAutorizacao { get; set; }
    }
}
