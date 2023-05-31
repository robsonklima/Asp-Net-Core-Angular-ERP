using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PontoUsuarioDataValidacao")]
    public partial class PontoUsuarioDataValidacao
    {
        [Key]
        public int CodPontoUsuarioDataValidacao { get; set; }
        public int CodPontoUsuarioData { get; set; }
        public int? CodPontoUsuarioDataJustificativaValidacao { get; set; }
        public string Observacao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }

        [ForeignKey(nameof(CodPontoUsuarioDataJustificativaValidacao))]
        [InverseProperty(nameof(PontoUsuarioDataJustificativaValidacao.PontoUsuarioDataValidacaos))]
        public virtual PontoUsuarioDataJustificativaValidacao CodPontoUsuarioDataJustificativaValidacaoNavigation { get; set; }
        [ForeignKey(nameof(CodPontoUsuarioData))]
        [InverseProperty(nameof(PontoUsuarioDatum.PontoUsuarioDataValidacaos))]
        public virtual PontoUsuarioDatum CodPontoUsuarioDataNavigation { get; set; }
    }
}
