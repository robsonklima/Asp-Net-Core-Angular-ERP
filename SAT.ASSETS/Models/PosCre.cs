using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("POS_CRE")]
    public partial class PosCre
    {
        [Column("CNPJ")]
        public double? Cnpj { get; set; }
        [Column("VALOR")]
        public double? Valor { get; set; }
    }
}
