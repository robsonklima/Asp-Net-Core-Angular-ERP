using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PontoUsuarioDataJustificativaValidacao")]
    public partial class PontoUsuarioDataJustificativaValidacao
    {
        public PontoUsuarioDataJustificativaValidacao()
        {
            PontoUsuarioDataValidacaos = new HashSet<PontoUsuarioDataValidacao>();
        }

        [Key]
        public int CodPontoUsuarioDataJustificativaValidacao { get; set; }
        [Required]
        [StringLength(255)]
        public string Descricao { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }

        [InverseProperty(nameof(PontoUsuarioDataValidacao.CodPontoUsuarioDataJustificativaValidacaoNavigation))]
        public virtual ICollection<PontoUsuarioDataValidacao> PontoUsuarioDataValidacaos { get; set; }
    }
}
