using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("DataHoraTrab")]
    public partial class DataHoraTrab
    {
        [Key]
        public int CodDataHoraTrab { get; set; }
        [Column("CodPecaRE5114")]
        public int CodPecaRe5114 { get; set; }
        [Column("CodOSBancada")]
        public int CodOsbancada { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraTrabInicio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraTrabFim { get; set; }

        [ForeignKey("CodOsbancada,CodPecaRe5114")]
        [InverseProperty(nameof(OsbancadaPeca.DataHoraTrabs))]
        public virtual OsbancadaPeca Cod { get; set; }
    }
}
