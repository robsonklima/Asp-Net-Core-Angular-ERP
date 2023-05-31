using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("RegiaoEstadoPOS")]
    public partial class RegiaoEstadoPo
    {
        [Key]
        [Column("CodRegiaoEstadoPOS")]
        public int CodRegiaoEstadoPos { get; set; }
        [Required]
        [Column("NomeRegiaoEstadoPOS")]
        [StringLength(300)]
        public string NomeRegiaoEstadoPos { get; set; }
        [Required]
        [Column("CodIBGE")]
        [StringLength(30)]
        public string CodIbge { get; set; }
        [Column("CodEstadoPOS")]
        public int CodEstadoPos { get; set; }

        [ForeignKey(nameof(CodEstadoPos))]
        [InverseProperty(nameof(EstadoPo.RegiaoEstadoPos))]
        public virtual EstadoPo CodEstadoPosNavigation { get; set; }
    }
}
