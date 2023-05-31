using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PontoUsuarioDataDivergenciaObservacao")]
    public partial class PontoUsuarioDataDivergenciaObservacao
    {
        [Key]
        public int CodPontoUsuarioDataDivergenciaObservacao { get; set; }
        public int CodPontoUsuarioDataDivergencia { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }

        [ForeignKey(nameof(CodPontoUsuarioDataDivergencia))]
        [InverseProperty(nameof(PontoUsuarioDataDivergencium.PontoUsuarioDataDivergenciaObservacaos))]
        public virtual PontoUsuarioDataDivergencium CodPontoUsuarioDataDivergenciaNavigation { get; set; }
    }
}
