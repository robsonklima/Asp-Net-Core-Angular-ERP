using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcSla
    {
        [Column("CodOS")]
        public int CodOs { get; set; }
        public int? CodFilial { get; set; }
        public int? CodRegiao { get; set; }
        public int CodCliente { get; set; }
        [Required]
        [Column("StatusSLA")]
        [StringLength(6)]
        public string StatusSla { get; set; }
        [Column("DataHoraAberturaOS", TypeName = "datetime")]
        public DateTime? DataHoraAberturaOs { get; set; }
        public int? Ano { get; set; }
        public int? Mes { get; set; }
    }
}
