using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("InstalRessalva")]
    public partial class InstalRessalva
    {
        [Key]
        public int CodInstalRessalva { get; set; }
        public int CodInstalacao { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        public int CodInstalMotivoRes { get; set; }
        [StringLength(200)]
        public string Comentario { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataOcorrencia { get; set; }
        public byte IndAtivo { get; set; }
        public byte IndJustificativa { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }

        [ForeignKey(nameof(CodInstalMotivoRes))]
        [InverseProperty(nameof(InstalMotivoRe.InstalRessalvas))]
        public virtual InstalMotivoRe CodInstalMotivoResNavigation { get; set; }
        [ForeignKey(nameof(CodInstalacao))]
        [InverseProperty(nameof(Instalacao.InstalRessalvas))]
        public virtual Instalacao CodInstalacaoNavigation { get; set; }
    }
}
