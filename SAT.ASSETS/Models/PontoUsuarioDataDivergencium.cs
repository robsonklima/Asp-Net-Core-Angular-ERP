using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class PontoUsuarioDataDivergencium
    {
        public PontoUsuarioDataDivergencium()
        {
            PontoUsuarioDataDivergenciaObservacaos = new HashSet<PontoUsuarioDataDivergenciaObservacao>();
        }

        [Key]
        public int CodPontoUsuarioDataDivergencia { get; set; }
        public int CodPontoUsuarioData { get; set; }
        public int CodPontoUsuarioDataModoDivergencia { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        public int CodPontoUsuarioDataMotivoDivergencia { get; set; }
        public int? DivergenciaValidada { get; set; }

        [ForeignKey(nameof(CodPontoUsuarioDataModoDivergencia))]
        [InverseProperty(nameof(PontoUsuarioDataModoDivergencium.PontoUsuarioDataDivergencia))]
        public virtual PontoUsuarioDataModoDivergencium CodPontoUsuarioDataModoDivergenciaNavigation { get; set; }
        [ForeignKey(nameof(CodPontoUsuarioDataMotivoDivergencia))]
        [InverseProperty(nameof(PontoUsuarioDataMotivoDivergencium.PontoUsuarioDataDivergencia))]
        public virtual PontoUsuarioDataMotivoDivergencium CodPontoUsuarioDataMotivoDivergenciaNavigation { get; set; }
        [ForeignKey(nameof(CodPontoUsuarioData))]
        [InverseProperty(nameof(PontoUsuarioDatum.PontoUsuarioDataDivergencia))]
        public virtual PontoUsuarioDatum CodPontoUsuarioDataNavigation { get; set; }
        [InverseProperty(nameof(PontoUsuarioDataDivergenciaObservacao.CodPontoUsuarioDataDivergenciaNavigation))]
        public virtual ICollection<PontoUsuarioDataDivergenciaObservacao> PontoUsuarioDataDivergenciaObservacaos { get; set; }
    }
}
