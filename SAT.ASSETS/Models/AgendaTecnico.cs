using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("AgendaTecnico")]
    public partial class AgendaTecnico
    {
        [Column("codAgendaTecnico")]
        public int CodAgendaTecnico { get; set; }
        [Column("CodOS")]
        [StringLength(50)]
        public string CodOs { get; set; }
        public int? Indice { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Inicio { get; set; }
        public int? CodTecnico { get; set; }
        [StringLength(50)]
        public string Cliente { get; set; }
        [StringLength(15)]
        public string NumBanco { get; set; }
        [StringLength(50)]
        public string Municipio { get; set; }
        [Column("UF")]
        [StringLength(3)]
        public string Uf { get; set; }
        [StringLength(50)]
        public string LocalAtendimento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        [StringLength(50)]
        public string CodUsuarioManut { get; set; }
        [StringLength(50)]
        public string Cor { get; set; }
    }
}
