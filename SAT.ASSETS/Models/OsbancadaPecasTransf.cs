using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OSBancadaPecasTransf")]
    public partial class OsbancadaPecasTransf
    {
        [Key]
        public int CodTransf { get; set; }
        public int? CodTecnico { get; set; }
        [Column("CodOSBancada")]
        public int? CodOsbancada { get; set; }
        [Column("CodPecaRE5114")]
        public int? CodPecaRe5114 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraTransf { get; set; }
        [StringLength(20)]
        public string CodUsuarioTransf { get; set; }

        [ForeignKey(nameof(CodTecnico))]
        [InverseProperty(nameof(Tecnico.OsbancadaPecasTransfs))]
        public virtual Tecnico CodTecnicoNavigation { get; set; }
    }
}
