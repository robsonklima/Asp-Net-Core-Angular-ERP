using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ParametrosPOS")]
    public partial class ParametrosPo
    {
        [Key]
        [Column("CodParametrosPOS")]
        public int CodParametrosPos { get; set; }
        [Column("DivideChamadosCorretivoPOS_SMART_CDS")]
        public bool DivideChamadosCorretivoPosSmartCds { get; set; }
        [Column("DivideChamadosIntegradosPOS_SMART_CDS")]
        public bool DivideChamadosIntegradosPosSmartCds { get; set; }
    }
}
