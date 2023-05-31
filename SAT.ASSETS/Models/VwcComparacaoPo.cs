using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcComparacaoPo
    {
        [Column("CNPJ")]
        [StringLength(30)]
        public string Cnpj { get; set; }
        [Column("QTDE")]
        public int? Qtde { get; set; }
    }
}
