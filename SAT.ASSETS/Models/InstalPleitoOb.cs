using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class InstalPleitoOb
    {
        [Key]
        public int CodInstalPleitoObs { get; set; }
        public int CodInstalPleito { get; set; }
        [Required]
        [StringLength(500)]
        public string Observacao { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        public byte IndAtivo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }

        [ForeignKey(nameof(CodInstalPleito))]
        [InverseProperty(nameof(InstalPleito.InstalPleitoObs))]
        public virtual InstalPleito CodInstalPleitoNavigation { get; set; }
    }
}
