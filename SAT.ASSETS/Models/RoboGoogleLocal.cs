using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("RoboGoogleLocal")]
    public partial class RoboGoogleLocal
    {
        public int CodPosto { get; set; }
        [StringLength(250)]
        public string Endereco { get; set; }
        [StringLength(50)]
        public string Numero { get; set; }
        [StringLength(50)]
        public string Bairro { get; set; }
        [StringLength(50)]
        public string Cidade { get; set; }
        [StringLength(50)]
        public string Estado { get; set; }
        [StringLength(50)]
        public string Pais { get; set; }
        [StringLength(50)]
        public string LatitudeGoogle { get; set; }
        [StringLength(50)]
        public string LongitudeGoogle { get; set; }
        [Column("EnderecoSAT")]
        [StringLength(250)]
        public string EnderecoSat { get; set; }
        [Column("LatitudeSAT")]
        [StringLength(50)]
        public string LatitudeSat { get; set; }
        [Column("LongitudeSAT")]
        [StringLength(50)]
        public string LongitudeSat { get; set; }
        public double? Distancia { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
    }
}
