using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("CheckInCheckOut")]
    public partial class CheckInCheckOut
    {
        public int CodCheckInCheckOut { get; set; }
        [Required]
        [StringLength(8)]
        public string Tipo { get; set; }
        [StringLength(20)]
        public string Modalidade { get; set; }
        [Column("CodOS")]
        public int? CodOs { get; set; }
        public int? CodRat { get; set; }
        [StringLength(50)]
        public string Latitude { get; set; }
        [StringLength(50)]
        public string Longitude { get; set; }
        [StringLength(30)]
        public string CodUsuarioTecnico { get; set; }
        [StringLength(30)]
        public string CodUsuarioPa { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCadSmartphone { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [StringLength(20)]
        public string DistanciaLocalMetros { get; set; }
        [StringLength(50)]
        public string DistanciaLocalDescricao { get; set; }
        [StringLength(20)]
        public string DuracaoLocalSegundos { get; set; }
        [StringLength(50)]
        public string DuracaoLocalDescricao { get; set; }
    }
}
