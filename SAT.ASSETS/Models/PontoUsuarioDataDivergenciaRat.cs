using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PontoUsuarioDataDivergenciaRat")]
    public partial class PontoUsuarioDataDivergenciaRat
    {
        [Key]
        public int CodPontoUsuarioDataDivergenciaRat { get; set; }
        public int CodPontoUsuarioData { get; set; }
        [Column("CodRAT")]
        public int CodRat { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }

        [ForeignKey(nameof(CodPontoUsuarioData))]
        [InverseProperty(nameof(PontoUsuarioDatum.PontoUsuarioDataDivergenciaRats))]
        public virtual PontoUsuarioDatum CodPontoUsuarioDataNavigation { get; set; }
    }
}
