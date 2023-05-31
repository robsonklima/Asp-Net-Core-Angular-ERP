using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcIndicadoresLogisticaProgramadorFilial
    {
        [Column("PA")]
        [StringLength(250)]
        public string Pa { get; set; }
        [Required]
        [Column("FILIAL")]
        [StringLength(50)]
        public string Filial { get; set; }
        [Column("DESCSTATUS")]
        [StringLength(50)]
        public string Descstatus { get; set; }
        [Column("QTDELIB")]
        public int? Qtdelib { get; set; }
    }
}
