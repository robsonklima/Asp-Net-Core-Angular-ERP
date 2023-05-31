using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("RATDigitalizada")]
    public partial class Ratdigitalizadum
    {
        [Key]
        public int CodRatDigitalizada { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataTransmissao { get; set; }
        [StringLength(50)]
        public string TipoTransmissao { get; set; }
    }
}
