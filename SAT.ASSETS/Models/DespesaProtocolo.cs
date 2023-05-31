using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("DespesaProtocolo")]
    public partial class DespesaProtocolo
    {
        [Key]
        public int CodDespesaProtocolo { get; set; }
        public int? CodFilial { get; set; }
        [Required]
        [StringLength(30)]
        public string NomeProtocolo { get; set; }
        [StringLength(300)]
        public string ObsProtocolo { get; set; }
        public byte IndAtivo { get; set; }
        public byte? IndFechamento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraFechamento { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        public int IndIntegracao { get; set; }
        public byte IndImpresso { get; set; }

        [ForeignKey(nameof(CodUsuarioCad))]
        [InverseProperty(nameof(Usuario.DespesaProtocoloCodUsuarioCadNavigations))]
        public virtual Usuario CodUsuarioCadNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioManut))]
        [InverseProperty(nameof(Usuario.DespesaProtocoloCodUsuarioManutNavigations))]
        public virtual Usuario CodUsuarioManutNavigation { get; set; }
    }
}
