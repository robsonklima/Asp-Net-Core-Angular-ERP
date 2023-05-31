using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ProducaoVeloh3")]
    public partial class ProducaoVeloh3
    {
        [Key]
        public int CodProducaoVeloh3 { get; set; }
        [Required]
        [StringLength(50)]
        public string NumeroSerie { get; set; }
        public int CodTipoVeloh3 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataProducao { get; set; }
        [Required]
        [Column("OpPQM")]
        [StringLength(50)]
        public string OpPqm { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataIntegracao { get; set; }
        [Column("IdOPSerie")]
        public int IdOpserie { get; set; }

        [ForeignKey(nameof(CodTipoVeloh3))]
        [InverseProperty(nameof(TipoVeloh3.ProducaoVeloh3s))]
        public virtual TipoVeloh3 CodTipoVeloh3Navigation { get; set; }
    }
}
