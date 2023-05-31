using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("GPSCoordenadaTecnico")]
    public partial class GpscoordenadaTecnico
    {
        [Key]
        [Column("CodGPSCoordenadaTecnico")]
        public int CodGpscoordenadaTecnico { get; set; }
        [StringLength(40)]
        public string CodTecnico { get; set; }
        [StringLength(40)]
        public string SimCardMobile { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraSatelite { get; set; }
        [StringLength(50)]
        public string LatitudePonto { get; set; }
        [StringLength(50)]
        public string LongitudePonto { get; set; }
        [Column(TypeName = "numeric(6, 2)")]
        public decimal? VelocidadePonto { get; set; }
        [Column(TypeName = "numeric(6, 2)")]
        public decimal? PrecisaoLocal { get; set; }
        [Column(TypeName = "numeric(6, 2)")]
        public decimal? NumSatelites { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
    }
}
