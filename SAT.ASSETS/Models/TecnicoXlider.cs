using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("TecnicoXLider")]
    public partial class TecnicoXlider
    {
        [Column("CodTecnicoXLider")]
        public int CodTecnicoXlider { get; set; }
        [Required]
        [StringLength(50)]
        public string CodUsuarioLider { get; set; }
        public int CodTecnico { get; set; }
        public int IndAtivo { get; set; }
        [Required]
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(50)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
    }
}
