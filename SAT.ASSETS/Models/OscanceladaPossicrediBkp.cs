using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("OSCanceladaPOSSicredi_BKP")]
    public partial class OscanceladaPossicrediBkp
    {
        [Column("CodOSCanceladaPOSSicredi")]
        public int CodOscanceladaPossicredi { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataCancelamento { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCancelamento { get; set; }
    }
}
