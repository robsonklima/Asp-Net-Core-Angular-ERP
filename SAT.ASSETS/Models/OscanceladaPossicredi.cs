using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OSCanceladaPOSSicredi")]
    public partial class OscanceladaPossicredi
    {
        [Key]
        [Column("CodOSCanceladaPOSSicredi")]
        public int CodOscanceladaPossicredi { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataCancelamento { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCancelamento { get; set; }

        [ForeignKey(nameof(CodOs))]
        [InverseProperty(nameof(O.OscanceladaPossicredi))]
        public virtual O CodOsNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioCancelamento))]
        [InverseProperty(nameof(Usuario.OscanceladaPossicredis))]
        public virtual Usuario CodUsuarioCancelamentoNavigation { get; set; }
    }
}
